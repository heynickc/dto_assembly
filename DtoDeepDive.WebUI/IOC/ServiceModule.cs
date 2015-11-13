using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DtoDeepDive.Data;
using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.Repository;
using DtoDeepDive.Data.Service;
using Ninject.Modules;

namespace DtoDeepDive.WebUI.IOC {
    public class ServiceModule : NinjectModule {
        public override void Load() {
            Bind<IPartsCatalogDbContext>().To<PartsCatalogDbContext>();
            Bind<IRepository<Part>>().To<PartRepository>();
            Bind<IPartCatalogService>().To<PartCatalogService>();
        }
    }
}