using System;
using DtoDeepDive.Data;
using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.DTO;
using DtoDeepDive.Data.Repository;
using DtoDeepDive.Data.Service;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace DtoDeepDive.UnitTests.PartCatalog {
    public class Part_catalog_dto_assembler : IDisposable {
        private readonly ITestOutputHelper _output;
        private readonly IPartsCatalogDbContext _db;
        public Part_catalog_dto_assembler(ITestOutputHelper output) {
            _output = output;
            _db = new FakePartsCatalogDbContext();

            for (int i = 0; i < 10; i++) {
                Part part = new Part() {
                    PartNumber = "TEST-PART-NUMBER|" + i,
                    ExtendedDescription = "I'M AN EXTENDED DESCRIPTION FOR " + "TEST-PART-NUMBER|" + i,
                    PartDescription = "I'M A SHORT DESCRIPTION FOR " + "TEST-PART-NUMBER|" + i,
                    SalesCode = "ABC",
                    UnitOfMeasure = "FT",
                    TotalQuantityRequired = 50
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
        public void get_part_dto_from_assembler() {
            var repository = new PartRepository(_db);
            var assembler = new PartAssembler();
            var partCatalogService = new PartCatalogService(repository, assembler);

            var partDto = partCatalogService.GetPart("TEST-PART-NUMBER|1");
            partDto.Should().BeOfType<PartDTO>();
        }
        [Fact]
        public void get_part_catalog_dto_from_assembler() {
            var repository = new PartRepository(_db);
            var assembler = new PartAssembler();
            var partCatalogService = new PartCatalogService(repository, assembler);

            var partCatalogDto = partCatalogService.GetPartCatalog();
            _output.WriteLine(partCatalogDto.ToJson());
            partCatalogDto.Should().BeOfType<PartCatalogDTO>();
        }
        public void Dispose() {
            _db.Dispose();
        }
    }
}
