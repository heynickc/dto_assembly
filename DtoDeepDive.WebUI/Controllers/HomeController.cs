using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DtoDeepDive.Data;
using DtoDeepDive.Data.Service;
using DtoDeepDive.Data.DTO;
using DtoDeepDive.WebUI.ViewModels;
using MediatR;
using DtoDeepDive.WebUI.Views;

namespace DtoDeepDive.WebUI.Controllers {
    public class HomeController : Controller {
        private readonly IMediator _mediator;
        public HomeController(IMediator mediator) {
            _mediator = mediator;
        }
        //private readonly IPartCatalogService _partCatalogService;
        //public HomeController(IPartCatalogService partCatalogService) {
        //    _partCatalogService = partCatalogService;
        //}
        //public ActionResult Index() {
        //    var partDtos = _partCatalogService.GetAllParts();
        //    var catalogViewModel = new Catalog();
        //    foreach (var partDto in partDtos) {
        //        catalogViewModel.Items.Add(new CatalogItem() {
        //            PartNumber = partDto.PartNumber,
        //            ExtendedDescription = partDto.ExtendedDescription
        //        });
        //    }
        //    return View(catalogViewModel);
        //}

        public ActionResult Index() {
            var catalogViewModel = _mediator.Send(new Index.Query());

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

        //public ActionResult Quote(Catalog catalog) {
        //    var partDtos = catalog.Items
        //        .Where(i => i.Selected)
        //        .Select(item => new PartDTO() {
        //            PartNumber = item.PartNumber,
        //            Quantity = item.Quantity
        //        }).ToList();
        //    var quoteDto = _partCatalogService.GetQuote(partDtos);
        //    return View(quoteDto);
        //}
    }
}