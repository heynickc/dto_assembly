using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.DTO;
using DtoDeepDive.Data.Repository;
using DtoDeepDive.Data.Service;
using DtoDeepDive.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xunit;
using Xunit.Abstractions;

namespace DtoDeepDive.UnitTests.WebUI {
    public class Home_controller_actions {
        private readonly ITestOutputHelper _output;
        private readonly IPartsCatalogDbContext _db;
        public Home_controller_actions(ITestOutputHelper output) {
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
        public void Home_controller_get_part_catalog() {
            var repository = new PartRepository(_db);
            var assembler = new PartAssembler();
            var partCatalogService = new PartCatalogService(repository, assembler);
            var controller = new HomeController(partCatalogService);

            var result = controller.Index() as ViewResult;

            _output.WriteLine(result.ToJson());
        }
        [Fact]
        public void Home_controller_get_quote() {
            var repository = new PartRepository(_db);
            var assembler = new PartAssembler();
            var partCatalogService = new PartCatalogService(repository, assembler);
            var controller = new HomeController(partCatalogService);

            var selection = new PartCatalogDTO() {
                Parts = new List<PartDTO> {
                    new PartDTO() {
                        Selected = true,
                        Quantity = 50
                    }
                }
            };

            var result = controller.Quote(selection);

            _output.WriteLine(result.ToJson());
        }
    }
}
