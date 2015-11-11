using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoDeepDive.Data {
    public class PartDTO {
        public int Id { get; set; }
        public string PartNumber { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UnitOfMeasure { get; set; }
        public string ExtendedDescription { get; set; }
        public string PartDescription { get; set; }
        public string SalesCode { get; set; }
        public List<string> LaborSequenceList { get; set; }
        public List<string> ComponentList { get; set; }
    }
}
