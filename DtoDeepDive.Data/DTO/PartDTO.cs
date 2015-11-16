using System;
using System.Collections.Generic;
using System.Linq;
using DtoDeepDive.Data.DAL;

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
}
