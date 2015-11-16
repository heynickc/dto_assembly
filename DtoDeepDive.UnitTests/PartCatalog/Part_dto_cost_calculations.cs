using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.DTOAssemblers;
using DtoDeepDive.Data.Repository;
using DtoDeepDive.Data.Service;
using Xunit;
using Xunit.Abstractions;

namespace DtoDeepDive.UnitTests.PartCatalog {
    public class Part_dto_cost_calculations {
        private readonly ITestOutputHelper _output;
        private readonly IPartsCatalogDbContext _db;
        public Part_dto_cost_calculations(ITestOutputHelper output) {
            _output = output;
            _db = new FakePartsCatalogDbContext();
            Fixtures.SeedFakePartDb(_db);
        }
        [Fact]
        public void Calculating_total_cost_of_a_parts_components() {
            var repository = new PartRepository(_db);
            var assembler = new PartAssembler();
            var service = new PartCatalogService(repository, assembler);

            var partDto = service.GetPart("TEST-PART-NUMBER|0");
            partDto.Quantity = 50;

            _output.WriteLine(partDto.TotalComponentCost.ToString());
        }

    }
}
