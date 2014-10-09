using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel;

namespace PTAData.Repositories
{
    public abstract class BaseRepository : IDisposable
    {
        private bool _disposed;
        protected DbContext _context;

        #region Dispose
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing && _context != null)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

    public abstract class BaseRepository<TDbContext> : BaseRepository where TDbContext: DbContext
    {
        internal BaseRepository()
        {

        }

        public List<TEntity> Get<TEntity>() where TEntity : class
        {
            var property = typeof(TDbContext).GetProperties()
                .FirstOrDefault(p => p.PropertyType.GenericTypeArguments[0] == typeof(TEntity));

            var dbSet = property.GetValue(Context) as DbSet<TEntity>;

            return dbSet.IncludeAll().Cast<TEntity>().ToList();
        }

        public DbSet<TEntity> Get<TEntity>(bool bind) where TEntity : class
        {
            var property = typeof(TDbContext).GetProperties()
                .FirstOrDefault(p => p.PropertyType.GenericTypeArguments[0] == typeof(TEntity));

            var dbSet = property.GetValue(Context) as DbSet<TEntity>;

            return dbSet;
        }

        public TDbContext Context
        {
            get
            {
                if (_context == null)
                    _context = Activator.CreateInstance<TDbContext>();

                return (TDbContext)_context;
            }
        }
    }
}
