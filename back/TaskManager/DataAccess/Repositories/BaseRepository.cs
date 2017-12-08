using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Models.DataAccess;
using TaskManager.DataAccess.Repositories.Abstration;

namespace TaskManager.DataAccess.Repositories
{
    public abstract class BaseRepository<TDbModel, TContext> : IRepository<TDbModel>
        where TDbModel : DbModel
        where TContext : DbContext
    {
        protected readonly TContext DbContext;
        protected readonly DbSet<TDbModel> DbSet;

        protected BaseRepository(TContext context)
        {
            DbContext = context;
            DbSet = context.Set<TDbModel>();
        }

        ~BaseRepository()
        {
            Dispose(false);
        }

        protected bool IsDisposed { get; set; }

        public virtual async Task<TDbModel> GetAsync(int id)
        {
            return await DbSet.FindAsync(id).ConfigureAwait(false);
        }

        public virtual void Insert(TDbModel entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            DbSet.Add(entity);
        }

        public virtual void Update(TDbModel entity)
        {
            if (entity.Id == default(int))
            {
                Insert(entity);
                return;
            }

            entity.ModifiedDate = DateTime.UtcNow;
            DbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Update(TDbModel entity, params Expression<Func<TDbModel, object>>[] include)
        {
            if (entity.Id == default(int))
            {
                Insert(entity);
                return;
            }

            entity.ModifiedDate = DateTime.UtcNow;
            DbSet.Attach(entity);
            include.ToList().ForEach(o => DbContext.Entry(entity).Property(o).IsModified = true);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var item = await DbSet.FindAsync(id).ConfigureAwait(false);
            Delete(item);
        }

        public virtual void Delete(TDbModel entity)
        {
            if (DbContext.Entry(entity).State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            DbSet.Remove(entity);
        }

        public void Delete(IEnumerable<TDbModel> entities)
        {
            var items = entities as IList<TDbModel> ?? entities.ToList();

            foreach (var item in items)
            {
                if (DbContext.Entry(item).State == EntityState.Detached)
                {
                    DbSet.Attach(item);
                }
            }

            DbSet.RemoveRange(items);
        }

        public async Task<IEnumerable<TDbModel>> FindAsync(Expression<Func<TDbModel, bool>> filter,
            Func<IQueryable<TDbModel>, IOrderedQueryable<TDbModel>> orderBy = null,
            params Expression<Func<TDbModel, object>>[] include)
        {
            var query = PrepareQuery(filter, orderBy);

            query = include.Aggregate(query, (current, propName) => current.Include(propName));

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                DbContext.Dispose();
                GC.SuppressFinalize(this);
            }

            IsDisposed = true;
        }

        private IQueryable<TDbModel> PrepareQuery(Expression<Func<TDbModel, bool>> filter,
            Func<IQueryable<TDbModel>, IOrderedQueryable<TDbModel>> orderBy)
        {
            IQueryable<TDbModel> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }
    }
}
