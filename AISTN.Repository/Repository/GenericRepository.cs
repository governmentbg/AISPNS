using AISTN.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using System.Reflection;

namespace AISTN.Repository.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity, new()
    {
        private readonly AistnContextLoggable _db;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AistnContextLoggable db)
        {
            _db = db;
            _dbSet = _db.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include) => GetAllQuery(include).ToList();

        public TEntity GetById(Guid id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include) => GetAllQuery(include).FirstOrDefault(e => e.Id == id);

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>>? filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            var query = GetAllQuery(include);
            if (filter != null) query = query.Where(filter);

            return query;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Save(UserActivity userActivity)
        {
            _db.SaveChanges(userActivity);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            UpdateDateModified(entity);
            _dbSet.Update(entity);
        }

        public void Remove(Guid id)
        {
            TEntity entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
            _db.Entry(entity).State = EntityState.Deleted;
        }

        public void RemoveRange(IEnumerable<TEntity> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public bool Any(Expression<Func<TEntity, bool>>? expression)
        {
            return _dbSet.Any(expression);
        }

        private void UpdateDateModified(TEntity entity)
        {
            PropertyInfo dateModified = entity.GetType().GetProperty("DateModified");
            if (dateModified != null) dateModified.SetValue(entity, DateTime.Now);
        }

        private IQueryable<TEntity> GetAllQuery(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include)
        {
            var query = _dbSet.AsQueryable().AsNoTracking();
            if (include != null) query = include(query);
            return query;
        }
    }
}
