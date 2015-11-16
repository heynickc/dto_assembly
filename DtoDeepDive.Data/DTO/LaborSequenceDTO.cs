using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DtoDeepDive.Data.DTO {
    public class LaborSequenceDTO {
        public string SequenceNumber { get; set; }
        public string SequenceDescription { get; set; }
        public double RunTime { get; set; }
        public decimal LaborRate { get; set; }
        public decimal Burden { get; set; }
        public decimal LaborCost { get; set; }
        public decimal GetLaborCost(double quantity) {
            return ((decimal)RunTime * LaborRate) * Burden;
        }
    }
}
