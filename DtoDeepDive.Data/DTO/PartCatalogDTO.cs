using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoDeepDive.Data.DTO {
    public class PartCatalogDTO {
        public PartCatalogDTO() {
            Parts = new List<PartDTO>();
        }
        public List<PartDTO> Parts { get; set; }
        public decimal TotalCost { get; set; }
        public decimal TotalMaterialCost { get; set; }
        public decimal TotalLaborCost { get; set; }
    }
}
