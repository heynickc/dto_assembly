using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoDeepDive.Data;
using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.Repository;
using DtoDeepDive.WebUI.IOC;
using FluentAssertions;
using Ninject;
using Xunit;

namespace DtoDeepDive.UnitTests {
    public class Web_ui_ioc {
        [Fact]
        public void Ninject_service_module_binds_correctly() {
            using (var kernel = new StandardKernel(new ServiceModule())) {
                var repository = kernel.Get<IRepository<Part>>();
                repository.Should().BeOfType<PartRepository>();
            }
        }
    }
}
