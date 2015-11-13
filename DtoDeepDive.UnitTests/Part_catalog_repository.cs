using System.Linq;
using DtoDeepDive.Data;
using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.Repository;
using Xunit;
using Xunit.Abstractions;

namespace DtoDeepDive.UnitTests {
    public class Part_catalog_repository {
        private readonly ITestOutputHelper _output;
        private readonly IPartsCatalogDbContext _db;
        public Part_catalog_repository(ITestOutputHelper output) {
            _output = output;
            _db = new FakePartsCatalogDbContext();
            for (int i = 0; i < 10; i++) {
                Part part = new Part() {
                    PartNumber = "TEST-PART-NUMBER|" + i
                };
                for (int j = 0; j < 5; j++) {
                    var component = new Component() {
                        ComponentNumber = "TEST-COMPONENT-NUMBER|" + j
                    };
                    part.Components.Add(component);
                }
                for (int k = 0; k < 5; k++) {
                    var labor = new LaborSequence() {
                        LaborSequenceNumber = "LABOR-SEQUENCE-NUMBER|" + k
                    };
                    part.LaborSequences.Add(labor);
                }
                _db.Parts.Add(part);
            }
            _db.SaveChanges();
        }
        [Fact]
        public void when_getting_all_parts_from_repository() {
            var repository = new PartRepository(_db);
            var parts = repository.GetAll(x => x.PartNumber != null);
            _output.WriteLine(parts.ToJson());
        }
        public void Dispose() {
            _db.Dispose();
        }
    }
}