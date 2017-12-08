using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Core.Models.DataAccess;

namespace TaskManager.DataAccess.Repositories.Abstration
{
    public interface IRepository<TDbModel> : IDisposable where TDbModel : DbModel
    {
        Task<TDbModel> GetAsync(int id);

        void Insert(TDbModel entity);

        void Update(TDbModel entity);

        void Update(TDbModel entity, params Expression<Func<TDbModel, object>>[] include);

        Task DeleteAsync(int id);

        void Delete(TDbModel entity);

        void Delete(IEnumerable<TDbModel> entities);

        Task<IEnumerable<TDbModel>> FindAsync(
            Expression<Func<TDbModel, bool>> filter,
            Func<IQueryable<TDbModel>, IOrderedQueryable<TDbModel>> orderBy = null,
            params Expression<Func<TDbModel, object>>[] include);
    }
}
