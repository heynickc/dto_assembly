using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoDeepDive.Data.DTO {
    public class PartCatalogDTO {
        public List<PartDTO> Parts { get; set; }
        public double TotalCost { get; set; }
        public double TotalMaterialCost { get; set; }
        public double TotalLaborCost { get; set; }
        public double TotalWeight { get; set; }
    }
}
