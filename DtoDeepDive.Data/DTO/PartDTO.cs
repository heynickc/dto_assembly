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
        public decimal TotalMaterialCost { get; set; }
        public decimal TotalLaborCost { get; set; }
        public bool Selected { get; set; }
        public double Quantity { get; set; }
        public void CalculateTotalCost(double quantity) {
            this.Quantity = quantity;
            foreach (var component in Components) {
                component.CalculateMaterialCost(quantity);
            }
            foreach (var labor in Labor) {
                labor.CalculateTotalLaborCost(quantity);
            }
        }
    }
}
