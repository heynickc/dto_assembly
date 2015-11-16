using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoDeepDive.Data.DTO {
    public class ComponentDTO {
        public string Number { get; set; }
        public string Description { get; set; }
        public string Material { get; set; }
        public string UnitOfMeasure { get; set; }
        public double QuantityPerAssembly { get; set; }
        public decimal CostPerUnit { get; set; }
        public double GetQuantityRequired(double quantity) {
            return quantity * QuantityPerAssembly;
        }
        public decimal GetComponentCost(double quantity) {
            var quantityRequired = quantity * QuantityPerAssembly;
            return (decimal)(quantityRequired) * CostPerUnit;
        }
    }
}
