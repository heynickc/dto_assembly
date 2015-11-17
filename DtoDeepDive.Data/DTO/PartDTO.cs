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
        public decimal TotalComponentCost { get { return Components.Sum(x => x.GetComponentCost(Quantity)); } }
        public decimal TotalLaborCost { get { return Labor.Sum(x => x.GetLaborCost(Quantity)); } }
        public decimal TotalCost { get { return TotalComponentCost + TotalLaborCost; } }
    }

    public class Query : IRequest<PartDTO> {
        public string PartNumber { get; set; }
    }
    public class Handler : IRequestHandler<Query, PartDTO> {
        private readonly IPartsCatalogDbContext _db;
        public Handler(IPartsCatalogDbContext db) {
            _db = db;
        }
        public PartDTO Handle(Query message) {
            var part = _db.Parts.SingleOrDefault(x => x.PartNumber == message.PartNumber);

            var partDto = new PartDTO();
            partDto.PartNumber = part.PartNumber;
            partDto.UnitOfMeasure = part.UnitOfMeasure;
            partDto.ExtendedDescription = part.ExtendedDescription;
            partDto.PartDescription = part.PartDescription;
            partDto.SalesCode = part.SalesCode;
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
            partDto.Components = componentsList;
            partDto.Labor = laborSequenceList;

            return partDto;
        }
    }
}
