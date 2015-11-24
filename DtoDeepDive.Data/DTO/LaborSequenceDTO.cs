using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoDeepDive.Data.DTO {
    public class LaborSequenceDTO {
        public string LaborSequenceNumber { get; set; }
        public string LaborSequenceDesc { get; set; }
        public double RunTime { get; set; }
        public decimal LaborRate { get; set; }
        public decimal Burden { get; set; }
        public decimal GetLaborCost(double quantity) {
            if (quantity > 0) {
                return ((decimal)(RunTime * quantity) * LaborRate) + Burden;
            }
            return 0;
        }
    }
}
