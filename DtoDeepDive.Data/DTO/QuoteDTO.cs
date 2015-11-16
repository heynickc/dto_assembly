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
        public decimal TotalCost { get { return TotalMaterialCost + TotalLaborCost; } }
        public decimal TotalMaterialCost { get { return Parts.Sum(x => x.TotalComponentCost); } }
        public decimal TotalLaborCost { get { return Parts.Sum(x => x.TotalLaborCost); } }
    }
}
