using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.DTO;

namespace DtoDeepDive.UnitTests.PartCatalog {
    public static class Fixtures {
        public static IPartsCatalogDbContext SeedFakePartDb(IPartsCatalogDbContext db) {
            var parts = BuildMultipleParts();
            db.Parts.AddRange(parts);
            db.SaveChanges();

            return db;
        }

        public static IQueryable<Part> BuildMultipleParts() {
            var parts = new List<Part>();
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
                parts.Add(part);
            }
            return parts.AsQueryable();
        }

        public static Part BuildSinglePart() {
            Part part = new Part() {
                PartNumber = "TEST-PART-NUMBER|01",
                ExtendedDescription = "I'M AN EXTENDED DESCRIPTION FOR " + "TEST-PART-NUMBER|01",
                PartDescription = "I'M A SHORT DESCRIPTION FOR " + "TEST-PART-NUMBER|01",
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
            return part;
        }
    }
}
