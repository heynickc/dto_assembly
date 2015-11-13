using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using DtoDeepDive.Data.DAL;

namespace DtoDeepDive.Data.Repository {
    public class PartRepository : IRepository<Part> {
        private readonly IPartsCatalogDbContext _dbContext;
        public PartRepository(IPartsCatalogDbContext dbContext) {
            _dbContext = dbContext;
        }
        public IQueryable<Part> GetAll(Expression<Func<Part, bool>> predicate = null) {
            return _dbContext.Parts.Where(predicate);
        }
        public Part Get(Expression<Func<Part, bool>> predicate) {
            return _dbContext.Parts.SingleOrDefault(predicate);
        }
        public void Add(Part entity) {
            _dbContext.Parts.Add(entity);
        }
        public void Update(Part entity) {
            _dbContext.Parts.Attach(entity);
            ((IObjectContextAdapter)_dbContext).ObjectContext.ObjectStateManager.ChangeObjectState(entity,
                EntityState.Modified);
        }
        public void Delete(Part entity) {
            _dbContext.Parts.Remove(entity);
        }
        public long Count() {
            return _dbContext.Parts.Count();
        }
    }
}
