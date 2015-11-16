using System;
using System.Collections.Generic;
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
        public List<LaborSequenceDTO> Labor { get; set; }
        public List<ComponentDTO> Components { get; set; }
        public decimal TotalComponentCost { get; set; }
        public decimal TotalLaborCost { get; set; }
        public void CalculateTotalCost(double quantity) {
            foreach (var component in Components) {
                component.CalculateComponentCost(quantity);
                TotalComponentCost += component.ComponentCost;
            }
            foreach (var labor in Labor) {
                labor.CalculateTotalLaborCost(quantity);
                TotalLaborCost += labor.LaborCost;
            }
        }
    }
}
