using DtoDeepDive.Data.Service;
using DtoDeepDive.UnitTests.IOC;
using FluentAssertions;
using Ninject;
using Xunit;

namespace DtoDeepDive.UnitTests.PartCatalog {
    public class Part_catalog_web_ui_ioc {
        [Fact]
        public void Ninject_service_module_binds_correctly() {
            //using (var kernel = new StandardKernel(new ServiceModule())) {
            //    var repository = kernel.Get<IPartCatalogService>();
            //    repository.Should().BeOfType<PartCatalogService>();
            //}
        }
    }
}
