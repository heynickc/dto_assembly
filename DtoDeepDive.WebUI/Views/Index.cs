using DtoDeepDive.Data.DTO;
using DtoDeepDive.Data.QueryDbContext;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DtoDeepDive.WebUI.Views {
    public class Index {
        public class Query : IRequest<List<PartDTO>> {
        }
        public class QueryHandler : IRequestHandler<Query, List<PartDTO>> {
            private readonly PartsCatalogQueryDbContext _db;
            public QueryHandler(PartsCatalogQueryDbContext db) {
                _db = db;
            }
            public List<PartDTO> Handle(Query message) {

                var result = _db.Parts.Select(p => new PartDTO {
                    PartNumber = p.PartNumber,
                    UnitOfMeasure = p.UnitOfMeasure,
                    ExtendedDescription = p.ExtendedDescription,
                    PartDescription = p.PartDescription,
                    SalesCode = p.SalesCode,
                    Components = p.Components
                        .Select(component => new ComponentDTO() {
                            Number = component.ComponentNumber,
                            Description = component.ComponentDescription,
                            Material = component.Material,
                            UnitOfMeasure = component.UnitOfMeasure,
                            QuantityPerAssembly = component.QuantityPerAssembly,
                            CostPerUnit = component.CostPerUnit
                        }).ToList(),
                    Labor = p.LaborSequences
                        .Select(labor => new LaborSequenceDTO() {
                            LaborSequenceNumber = labor.LaborSequenceNumber,
                            LaborSequenceDesc = labor.LaborSequenceDesc,
                            RunTime = labor.RunTime,
                            LaborRate = labor.LaborRate,
                            Burden = labor.Burden
                        }).ToList(),
                }).ToList();

                return result;
            }
        }
    }
}