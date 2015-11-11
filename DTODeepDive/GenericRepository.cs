using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DtoDeepDive.Data {
    class Repository<T> : IRepository<T> where T : class {
        private readonly PartsCatalogDbContext _dbContext = null;
        private readonly DbSet<T> _dbSet;
        public Repository(PartsCatalogDbContext context) {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null) {
            if (predicate != null) {
                return _dbSet.Where(predicate);
            }
            return _dbSet.AsQueryable();
        }
        public T Get(Expression<Func<T, bool>> predicate) {
            return _dbSet.FirstOrDefault(predicate);
        }
        public void Add(T entity) {
            _dbSet.Add(entity);
        }
        public void Update(T entity) {
            _dbSet.Attach(entity);
            ((IObjectContextAdapter)_dbContext).ObjectContext.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
        }
        public void Delete(T entity) {
            _dbSet.Remove(entity);
        }
        public long Count() {
            return _dbSet.Count();
        }
    }
}
