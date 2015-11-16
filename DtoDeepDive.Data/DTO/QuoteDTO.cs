using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoDeepDive.Data.DTO {
    public class QuoteDTO {
        public QuoteDTO() {
            Parts = new List<PartDTO>();
        }
        public List<PartDTO> Parts { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalMaterialCost { get; set; }
        public decimal TotalLaborCost { get; set; }
        public void CalculateTotalCosts() {
            TotalMaterialCost = Parts.Sum(x => x.TotalComponentCost);
            TotalLaborCost = Parts.Sum(x => x.TotalLaborCost);
            TotalCost = TotalMaterialCost + TotalLaborCost;
        }
    }
}
