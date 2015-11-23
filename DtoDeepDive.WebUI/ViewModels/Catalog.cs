using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DtoDeepDive.WebUI.ViewModels {
    public class Catalog {
        public Catalog() {
            this.Items = new List<CatalogItem>();
        }
        public List<CatalogItem> Items { get; set; }
        public List<CatalogItem> getSelectedItems() {
            return Items.Where(x => x.Selected).ToList();
        }
    }
}