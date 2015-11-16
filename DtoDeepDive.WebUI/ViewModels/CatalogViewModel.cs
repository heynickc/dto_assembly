using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DtoDeepDive.WebUI.ViewModels {
    public class CatalogViewModel {
        public CatalogViewModel() {
            this.Items = new List<CatalogItemViewModel>();
        }
        public List<CatalogItemViewModel> Items { get; set; }
        public List<CatalogItemViewModel> getSelectedItems() {
            return Items.Where(x => x.Selected).ToList();
        }
    }
}