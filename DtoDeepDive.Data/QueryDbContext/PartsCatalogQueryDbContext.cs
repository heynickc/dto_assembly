using DtoDeepDive.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoDeepDive.Data.QueryDbContext {
    public class PartsCatalogQueryDbContext : IDisposable {
        private readonly IPartsCatalogDbContext _db;
        public PartsCatalogQueryDbContext() {
            _db = new PartsCatalogDbContext();
        }
        public IQueryable<Part> Parts {
            get { return _db.Parts; }
        }
        public IQueryable<Component> Components {
            get { return _db.Components; }
        }
        public IQueryable<LaborSequence> LaborSequences {
            get { return _db.LaborSequences; }
        }
        public void Dispose() {
            _db.Dispose();
        }
    }
}
