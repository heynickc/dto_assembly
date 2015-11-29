using DtoDeepDive.Data.DTO;
using DtoDeepDive.Data.QueryDbContext;
using DtoDeepDive.WebUI.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DtoDeepDive.WebUI.Views {
    public class Index {
        public class Query : IRequest<CatalogViewModel> {
        }
        public class QueryHandler : IRequestHandler<Query, CatalogViewModel> {
            private readonly PartsCatalogQueryDbContext _db;
            public QueryHandler(PartsCatalogQueryDbContext db) {
                _db = db;
            }

            public CatalogViewModel Handle(Query message) {
                var catalogViewModel = new CatalogViewModel();

                catalogViewModel.Items.AddRange(_db.Parts.Select(p => new CatalogItemViewModel() {
                    PartNumber = p.PartNumber,
                    ExtendedDescription = p.ExtendedDescription
                }));

                return catalogViewModel;
            }
        }
    }
}