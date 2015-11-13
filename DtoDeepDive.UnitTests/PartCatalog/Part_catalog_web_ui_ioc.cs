using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.Repository;
using DtoDeepDive.WebUI.IOC;
using FluentAssertions;
using Ninject;
using Xunit;

namespace DtoDeepDive.UnitTests.PartCatalog {
    public class Part_catalog_web_ui_ioc {
        [Fact]
        public void Ninject_service_module_binds_correctly() {
            using (var kernel = new StandardKernel(new ServiceModule())) {
                var repository = kernel.Get<IRepository<Part>>();
                repository.Should().BeOfType<PartRepository>();
            }
        }
    }
}
