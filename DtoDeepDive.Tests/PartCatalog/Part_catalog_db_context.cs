using System;
using System.Linq;
using DtoDeepDive.Data.DAL;
using Xunit;
using Xunit.Abstractions;

namespace DtoDeepDive.IntegrationTests.PartCatalog {
    public class Part_catalog_db_context : IDisposable {
        private readonly ITestOutputHelper _output;
        private readonly IPartsCatalogDbContext _db;
        public Part_catalog_db_context(ITestOutputHelper output) {
            _output = output;
            _db = new PartsCatalogDbContext();

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
                for (int j = 0; j < 5; j++) {
                    var component = new Component() {
                        ComponentNumber = "LABOR-SEQUENCE-NUMBER|" + j
                    };
                    part.Components.Add(component);
                }
                _db.Parts.Add(part);
            }
            _db.SaveChanges();
        }
        [Fact]
        public void when_getting_part_from_database() {
            var parts = _db.Parts.ToList();
            _output.WriteLine(parts.ToJson());
        }
        public void Dispose() {
            var parts = _db.Parts.ToList();
            _db.Parts.RemoveRange(parts);
            _db.SaveChanges();
            _db.Dispose();
        }
    }
}
