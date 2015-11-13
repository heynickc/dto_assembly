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
                    PartNumber = "TEST-PART-NUMBER|" + i,
                    ExtendedDescription = "I'M AN EXTENDED DESCRIPTION FOR " + "TEST-PART-NUMBER|" + i,
                    PartDescription = "I'M A SHORT DESCRIPTION FOR " + "TEST-PART-NUMBER|" + i,
                    SalesCode = "ABC",
                    UnitOfMeasure = "FT"
                };
                for (int j = 0; j < 5; j++) {
                    var component = new Component() {
                        ComponentNumber = "TEST-COMPONENT-NUMBER|" + j,
                        ComponentDescription = "I'M A COMPONENT CALLED " + "TEST-COMPONENT-NUMBER|" + j,
                        Material = "METAL",
                        UnitOfMeasure = "LBS",
                        CostPerUnit = 5.00m,
                        QuantityPerAssembly = 1.25,
                    };
                    part.Components.Add(component);
                }
                for (int k = 0; k < 5; k++) {
                    var labor = new LaborSequence() {
                        LaborSequenceNumber = "LABOR-SEQUENCE-NUMBER|" + k,
                        LaborSequenceDesc = "LABOR SEQUENCE DESCRIPTION FOR " + "LABOR-SEQUENCE-NUMBER|" + k,
                        RunTime = 2.0,
                        Burden = 10.00m,
                        LaborRate = 15.00m,
                        Facility = "PLANT 1",
                        Machine = "LASER CUTTER"
                    };
                    part.LaborSequences.Add(labor);
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
            //var parts = _db.Parts.ToList();
            //_db.Parts.RemoveRange(parts);
            //_db.SaveChanges();
            _db.Dispose();
        }
    }
}
