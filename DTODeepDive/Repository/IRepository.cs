using System;
using System.Linq;
using System.Linq.Expressions;

namespace DtoDeepDive.Data.Repository {
    public interface IRepository<T> where T : class {
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null);
        T Get(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        long Count();
    }
}
