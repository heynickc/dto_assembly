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
        public double QuantityRequired { get; set; }
        public decimal MaterialCost { get; set; }
    }
}
