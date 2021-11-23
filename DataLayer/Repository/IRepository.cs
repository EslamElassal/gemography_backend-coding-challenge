using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

 

namespace DataLayer.Repository
{
    public interface IRepository<T> where T : class
    {
        #region Props

        /// <summary>
        /// This Is The Current User Or The Logged In User Id
        /// </summary>
        public long UserId { get; }


        /// <summary>
        /// This Is The Current User Language Or The Logged In User Language Id
        /// </summary>
        public long CurrentLanguage { get; }

        #endregion

        #region Delete

        bool Delete(T entity);
        void DeleteWithoutSaveChange(T entity);
        bool DeleteRange(IEnumerable<T> entity);
        void DeleteRangeWithoutSaveChange(IEnumerable<T> entity);
        Task<bool> DeleteAsync(T entity);

        #endregion

        #region Insert

        bool Insert(T entity);
        bool InsertRange(IEnumerable<T> entities);

        void InsertWithoutSaveChange(T entity);
        void InsertRangeWithoutSaveChange(IEnumerable<T> entities);
        void Dispose();
        Task<bool> InsertAsync(T entity);

        #endregion

        #region Update

        bool Update(T entity);
        void UpdateWithoutSaveChange(T entity);
        Task<bool> UpdateAsync(T entity);

        #endregion

        #region Get
        int Count(Func<T, bool> predicate);
        long Max(Func<T, long> predicate);
        T Single(Func<T, bool> predicate);
         T SingleOrDefault(Func<T, bool> predicate)  ;

        T FirstOrDefault(Func<T, bool> predicate);
        T FirstOrDefault();
         
        IQueryable<T> GetAll();
        IQueryable<T> Take(int count);
        IQueryable<T> GetAllAsNoTracking();
        Task<IQueryable<T>> GetAllAsync();
        //Task<bool> AnyAsync<T>(Expression<Func<T, bool>> predicate);
        //bool Any<T>(Expression<Func<T, bool>> predicate);

        T GetById(object Id);
        IQueryable<T> Find(Func<T, bool> predicate);
        //Task<IQueryable<T>> FindAsync(Func<T, bool> predicate);

        T GetByIdDetached(object id);
        T Detached(T entity);

        #endregion

        #region Sql

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="U">The Result Class It Will Be List< <see cref="U"/> ></typeparam>
        /// <param name="query">Query string or Stored Procedure name</param>
        /// <param name="parameters">Parameters </param>
        /// <param name="commandType">Query or StoredProcedure default is StoredProcedure</param>
        /// <returns></returns> 
        List<U> ExecuteStoredProcedure<U>(string query, SqlParameter[] parameters = null, CommandType commandType = CommandType.StoredProcedure);

        #endregion

        #region Save

        bool SaveChange();
        Task<bool> SaveChangeAsnc();

        #endregion

        #region SqL Query
         bool ExecuteQuery(string query);
        #endregion
    }

}
