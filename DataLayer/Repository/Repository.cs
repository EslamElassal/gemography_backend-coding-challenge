 
 using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using DataLayer.DBEntities;
using Microsoft.Data.SqlClient;

namespace DataLayer.Repository
{
  /// <summary>
  /// Repository Pattern
  /// </summary>
  /// <typeparam name="T"> <see cref="T"/> is a class</typeparam>
  public class Repository<T> : IRepository<T> where T : class
    {

        #region Fields Props

        /// <summary>
        /// This Is The Current User Or The Logged In User Id
        /// </summary>
        public long UserId { get; }

        /// <summary>
        /// This Is The Current User Language Or The Logged In User Language Id
        /// </summary>
        public long CurrentLanguage { get; }

        // public DeviceEnum DeviceEnum { get; }

        public CUSERSESLAMELASSALSOURCEREPOSGEMOGRAPHY_BACKENDCODINGCHALLENGEDATALAYERMODELSGEMOGRAPHYDATABASEMDFContext Db;
        private readonly IUnitOfWork<CUSERSESLAMELASSALSOURCEREPOSGEMOGRAPHY_BACKENDCODINGCHALLENGEDATALAYERMODELSGEMOGRAPHYDATABASEMDFContext> _uow;
         private readonly DbSet<T> _data;

        public bool IsDisposed => _uow.IsDisposed;


        public void Dispose() => _uow.Dispose();


        public Repository(IUnitOfWork<CUSERSESLAMELASSALSOURCEREPOSGEMOGRAPHY_BACKENDCODINGCHALLENGEDATALAYERMODELSGEMOGRAPHYDATABASEMDFContext> unitOfWork
            
            /*AppOrMobileCall appOrMobileCall,ConfigurationBll configuration*/)
        
    {
            _uow = unitOfWork;
            Db = _uow.VDreamDbContext;
            _data = Db.Set<T>();

            
           
          }

        #endregion

        #region Delete

        /// <summary>
        /// Delete The Current Entity <see cref="T"/>
        /// </summary>
        /// <param name="entity"><see cref="T"/></param>
        /// <returns></returns>
        public virtual bool Delete(T entity)
        {
            _data.Remove(entity);
             return SaveChange();
        }

        /// <summary>
        /// Delete The Current Entity <see cref="T"/>
        /// </summary>
        /// <param name="entity"><see cref="T"/></param>
        /// <returns></returns>
        public virtual Task<bool> DeleteAsync(T entity)
        {
            _data.Remove(entity);
             return SaveChangeAsnc();
        }

        /// <summary>
        /// Delete The Current Entity <see cref="T"/> Without Save
        /// </summary>
        /// <param name="entity"><see cref="T"/></param>
        /// <returns></returns>
        public virtual void DeleteWithoutSaveChange(T entity)
        {
            _data.Remove(entity);
           
        }

        /// <summary>
        /// Delete The Current Entities <see cref="T"/>
        /// </summary>
        /// <param name="entity"><see cref="T"/></param>
        /// <returns></returns>
        public virtual bool DeleteRange(IEnumerable<T> entity)
        {
            _data.RemoveRange(entity);
         
            return SaveChange();
        }

        /// <summary>
        /// Delete The Current Entities <see cref="T"/> Without Save
        /// </summary>
        /// <param name="entity"><see cref="T"/></param>
        /// <returns></returns>
        public virtual void DeleteRangeWithoutSaveChange(IEnumerable<T> entity)
        {
            _data.RemoveRange(entity);
             
        }

        #endregion

        #region Insert

        public virtual bool Insert(T entity)
        {
            _data.Add(entity);
            if (!SaveChange()) return false;
            // after saving changing because i need ID of table
            
             SaveChange();
            return true;
        }

        public virtual Task<bool> InsertAsync(T entity)
        {
            _data.AddAsync(entity);
             return SaveChangeAsnc();
        }

        public virtual void InsertWithoutSaveChange(T entity)
        {
            _data.Add(entity);
         }

        public virtual bool InsertRange(IEnumerable<T> entities)
        {
            if (entities == null) return false;
            _data.AddRange(entities);
            if (!SaveChange()) return false;
            // after saving changing because i need ID of table
             SaveChange();
            return true;
        }

        public virtual void InsertRangeWithoutSaveChange(IEnumerable<T> entities)
        {
            _data.AddRange(entities);
         }

        #endregion

        #region Update

      

        public virtual bool Update(T entity)
        {
            Db.Entry(entity).CurrentValues.SetValues(entity);
            Db.Entry(entity).State = EntityState.Modified;
          

            return SaveChange();
        }

        public virtual Task<bool> UpdateAsync(T entity)
        {
            _data.Update(entity).State = EntityState.Modified;
            
            return SaveChangeAsnc();
        }

        public virtual void UpdateWithoutSaveChange(T entity)
        {
            _data.Update(entity).State = EntityState.Modified;
            
        }

        #endregion

        #region Get

        public virtual IQueryable<T> Find(Func<T, bool> predicate) => _data.Where(predicate).AsQueryable<T>();


        public virtual IQueryable<T> GetAll() => _data.AsQueryable();

        public virtual IQueryable<T> GetAllAsNoTracking() => _data.AsNoTracking().AsQueryable();


        public virtual async Task<IQueryable<T>> GetAllAsync() => await Task.FromResult(_data.AsQueryable());


        public virtual T GetById(object Id) => _data.Find(Id);

        public virtual T GetByIdDetached(object Id)
        {
            var data = GetById(Id);
            Db.Entry(data).State = EntityState.Detached;

            return data;
        }

        public virtual T Detached(T entity)
        {
            Db.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        #endregion

        #region Save

        public virtual bool SaveChange() => _uow.SaveChanges();

        public virtual Task<bool> SaveChangeAsnc() => _uow.SaveChangesAsync();

        #endregion

        #region SQL Query

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U">The Result Class It Will Be List< <see cref="U"/> ></typeparam>
        /// <param name="query">Query string or Stored Procedure name</param>
        /// <param name="parameters">Parameters </param>
        /// <param name="commandType">Query or StoredProcedure default is StoredProcedure</param>
        /// <returns></returns>
        public List<U> ExecuteStoredProcedure<U>(string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.StoredProcedure)
        {
            /// in _db.Database.GetDbConnection().ConnectionString if the password didn't get inside connection string
            /// please make sure you add => "Persist Security Info=true;" inside your connection string in appsetting.json
            SqlConnection sqlConnection = new SqlConnection(Db.Database.GetDbConnection().ConnectionString);
            SqlCommand cmd = new SqlCommand(query, sqlConnection);
            if (parameters != null)
                cmd.Parameters.AddRange(parameters);
            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();
            cmd.CommandType = commandType;
            var reader = cmd.ExecuteReader();

            DataTable tbl = new DataTable();
            tbl.Load(reader, LoadOption.PreserveChanges);
            sqlConnection.Close();
            return ConvertDataTable<U>(tbl);
        }

        private List<U> ConvertDataTable<U>(DataTable dt)
        {
            List<U> data = new List<U>();
            Type temp = typeof(U);
            foreach (DataRow row in dt.Rows) data.Add(GetItem<U>(row, temp));
            return data;
        }

        private TU GetItem<TU>(DataRow dr, Type temp)
        {
            TU obj = Activator.CreateInstance<TU>();
            foreach (DataColumn column in dr.Table.Columns)
            {
                PropertyInfo pro = temp.GetProperty(column.ColumnName);
                if (pro == null || !pro.CanWrite) continue;
                try
                {
                    object col = dr[column.ColumnName];
                    if (pro.PropertyType.Name == nameof(Boolean) && (col.ToString() == "0" || col.ToString() == "1")) col = col.ToString() != "0";
                    pro.SetValue(obj, col, null);
                }
                catch
                {
                    pro.SetValue(obj, null, null);
                }
            }
            return obj;
        }

        public bool ExecuteQuery(string query)
        {
            return Db.Database.ExecuteSqlRaw(query) >= 0;
        }

        public int Count(Func<T, bool> predicate) => _data.Count(predicate);


        public long Max(Func<T, long> predicate) => _data.Max(predicate);

 
        public virtual  T Single(Func<T, bool> predicate) => _data.Single(predicate);
        public virtual T SingleOrDefault(Func<T, bool> predicate) => _data.SingleOrDefault(predicate);
 
        public T FirstOrDefault(Func<T, bool> predicate) => _data.FirstOrDefault(predicate);

        public T FirstOrDefault() => _data.FirstOrDefault();

        public IQueryable<T> Take(int count) => _data.Take(count);

 








        #endregion
    }
}