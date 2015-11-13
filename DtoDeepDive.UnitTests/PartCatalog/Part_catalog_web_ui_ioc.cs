using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.Repository;
using DtoDeepDive.Data.Service;
using FluentAssertions;
using Ninject;
using Ninject.Modules;
using Xunit;

namespace DtoDeepDive.UnitTests.PartCatalog {
    public class Part_catalog_web_ui_ioc {
        [Fact]
        public void Ninject_service_module_binds_correctly() {
            using (var kernel = new StandardKernel(new ServiceModule())) {
                var repository = kernel.Get<IPartCatalogService>();
                repository.Should().BeOfType<PartCatalogService>();
            }
        }
    }
    public class ServiceModule : NinjectModule {
        public override void Load() {
            Bind<IPartsCatalogDbContext>().To<PartsCatalogDbContext>();
            Bind<IRepository<Part>>().To<PartRepository>();
            Bind<IPartCatalogService>().To<PartCatalogService>();
        }
    }
}
