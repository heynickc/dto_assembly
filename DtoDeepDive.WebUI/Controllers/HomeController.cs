using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DtoDeepDive.Data;
using DtoDeepDive.Data.Service;
using DtoDeepDive.Data.DTO;
using DtoDeepDive.WebUI.ViewModels;

namespace DtoDeepDive.WebUI.Controllers {
    public class HomeController : Controller {
        private readonly IPartCatalogService _partCatalogService;
        public HomeController(IPartCatalogService partCatalogService) {
            _partCatalogService = partCatalogService;
        }
        public ActionResult Index() {
            var partDtos = _partCatalogService.GetAllParts();
            var catalogViewModel = new CatalogViewModel();
            foreach (var partDto in partDtos) {
                catalogViewModel.Items.Add(new CatalogItemViewModel() {
                    PartNumber = partDto.PartNumber,
                    ExtendedDescription = partDto.ExtendedDescription
                });
            }
            return View(catalogViewModel);
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Quote(CatalogViewModel catalogViewModel) {
            var selectedItems = catalogViewModel.Items
                .Where(i => i.Selected)
                .Select(pn => new { pn.PartNumber, pn.Quantity })
                .ToDictionary(pn => pn.PartNumber, qty => qty.Quantity);
            var quoteDto = _partCatalogService.GetQuote(selectedItems);
            return View(quoteDto);
        }
    }
}