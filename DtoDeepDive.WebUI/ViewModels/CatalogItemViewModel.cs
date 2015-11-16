using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DtoDeepDive.WebUI.ViewModels {
    public class CatalogItemViewModel {
        public string PartNumber { get; set; }
        public string ExtendedDescription { get; set; }
        public bool Selected { get; set; }
        public double Quantity { get; set; }
    }
}