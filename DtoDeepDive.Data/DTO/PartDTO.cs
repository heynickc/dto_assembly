using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DtoDeepDive.Data.DAL;
using MediatR;
using DtoDeepDive.Data.QueryDbContext;

namespace DtoDeepDive.Data.DTO {
    public class PartDTO {
        public PartDTO() {
            this.Labor = new List<LaborSequenceDTO>();
            this.Components = new List<ComponentDTO>();
        }
        public string PartNumber { get; set; }
        public string ExtendedDescription { get; set; }
        public string PartDescription { get; set; }
        public string SalesCode { get; set; }
        public string UnitOfMeasure { get; set; }
        public double Quantity { get; set; }
        public List<LaborSequenceDTO> Labor { get; set; }
        public List<ComponentDTO> Components { get; set; }
        public decimal TotalComponentCost {
            get {
                return Components.Sum(x => x.GetComponentCost(Quantity));
            }
        }
        public decimal TotalLaborCost {
            get {
                return Labor.Sum(x => x.GetLaborCost(Quantity));
            }
        }
        public decimal TotalCost {
            get {
                return TotalComponentCost + TotalLaborCost;
            }
        }
    }
    public class Result {
        public Result() {
            PartDto = new PartDTO();
        }
        public PartDTO PartDto { get; set; }
    }
    public class Query : IRequest<Result> {
        public string PartNumber { get; set; }
    }
    public class Handler : IRequestHandler<Query, Result> {
        private readonly PartsCatalogQueryDbContext _db;
        public Handler(PartsCatalogQueryDbContext db) {
            _db = db;
        }
        public Result Handle(Query message) {

            var part = _db.Parts
                .Where(x => x.PartNumber == message.PartNumber);

            var result = new Result();
            result.PartDto = part.Select(p => new PartDTO {
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
            }).FirstOrDefault();

            return result;
        }
    }
}
