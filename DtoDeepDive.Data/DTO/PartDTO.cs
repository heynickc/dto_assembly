using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DtoDeepDive.Data.DAL;
using MediatR;

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
        private readonly PartsCatalogDbContext _db;
        public Handler(PartsCatalogDbContext db) {
            _db = db;
        }
        public Result Handle(Query message) {

            var part = _db.Parts
                .SingleOrDefault(x => x.PartNumber == message.PartNumber);

            var result = new Result();
            result.PartDto.PartNumber = part.PartNumber;
            result.PartDto.UnitOfMeasure = part.UnitOfMeasure;
            result.PartDto.ExtendedDescription = part.ExtendedDescription;
            result.PartDto.PartDescription = part.PartDescription;
            result.PartDto.SalesCode = part.SalesCode;
            var componentsList = part.Components
                .Select(component => new ComponentDTO() {
                    Number = component.ComponentNumber,
                    Description = component.ComponentDescription,
                    Material = component.Material,
                    UnitOfMeasure = component.UnitOfMeasure,
                    QuantityPerAssembly = component.QuantityPerAssembly,
                    CostPerUnit = component.CostPerUnit
                }).ToList();
            var laborSequenceList = part.LaborSequences
                .Select(labor => new LaborSequenceDTO() {
                    SequenceNumber = labor.LaborSequenceNumber,
                    SequenceDescription = labor.LaborSequenceDesc,
                    RunTime = labor.RunTime,
                    LaborRate = labor.LaborRate,
                    Burden = labor.Burden
                }).ToList();
            result.PartDto.Components = componentsList;
            result.PartDto.Labor = laborSequenceList;

            return result;
        }
    }

}
