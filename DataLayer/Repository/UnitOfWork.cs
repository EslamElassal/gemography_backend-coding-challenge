 using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using System;
using System.Threading.Tasks;


namespace DataLayer.Repository
{
    public class UnitOfWork<T> :IUnitOfWork<T> where T :DbContext
    {
        #region Fields

        bool _IsDisposed = false;

        public UnitOfWork(T VDreamDbContext) => this.VDreamDbContext = VDreamDbContext;

    #endregion

    #region Props
        public T VDreamDbContext { get; }
        public IDbContextTransaction DbContextTransaction { get; set; }

        public bool IsDisposed { get => _IsDisposed; }

       

        public T VDreamDBContext => throw new NotImplementedException();



        #endregion

        #region Methods

        public virtual void Commit() => VDreamDbContext.Database.CurrentTransaction.Commit();

        public bool SaveChanges()
        {
            try
            {
                return VDreamDbContext.SaveChanges() >= 0;
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                return false;
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                return (await VDreamDbContext.SaveChangesAsync()) > 0;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            VDreamDbContext.Dispose();
            _IsDisposed = true;
        }
        #endregion
    }

}
