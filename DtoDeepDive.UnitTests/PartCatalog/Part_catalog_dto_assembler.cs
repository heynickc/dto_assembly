using System;
using DtoDeepDive.Data;
using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.DTO;
using DtoDeepDive.Data.Repository;
using DtoDeepDive.Data.Service;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using System.Collections.Generic;
using DtoDeepDive.Data.DTOAssemblers;

namespace DtoDeepDive.UnitTests.PartCatalog {
    public class Part_catalog_dto_assembler : IDisposable {
        private readonly ITestOutputHelper _output;
        private readonly IPartsCatalogDbContext _db;
        public Part_catalog_dto_assembler(ITestOutputHelper output) {
            _output = output;
            _db = new FakePartsCatalogDbContext();
            Fixtures.SeedFakePartDb(_db);
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

            var partCatalogDto = partCatalogService.GetAllParts();
            _output.WriteLine(partCatalogDto.ToJson());
            partCatalogDto.Should().BeOfType<List<PartDTO>>();
        }
        public void Dispose() {
            _db.Dispose();
        }
    }
}
