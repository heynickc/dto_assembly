using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DtoDeepDive.Data;
using DtoDeepDive.Data.Service;

namespace DtoDeepDive.WebUI.Controllers {
    public class HomeController : Controller {
        private readonly IPartCatalogService _partCatalogService;
        public HomeController(IPartCatalogService partCatalogService) {
            _partCatalogService = partCatalogService;
        }
        public ActionResult Index() {
            var partCatalog = _partCatalogService.GetPartCatalog();
            return View(partCatalog);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}