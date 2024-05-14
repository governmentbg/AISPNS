using AISTN.Data;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace AISTN.Repository.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity, new()
    {
        IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        TEntity GetById(Guid id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);
        void Save();
        void Save(UserActivity userActivity);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(Guid id);
        void RemoveRange(IEnumerable<TEntity> entity);
        bool Any(Expression<Func<TEntity, bool>>? expression = null);
    }
}
