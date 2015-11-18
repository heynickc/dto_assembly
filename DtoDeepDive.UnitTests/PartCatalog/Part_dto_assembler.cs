using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtoDeepDive.Data.DTO;
using DtoDeepDive.Data.DTOAssemblers;
using DtoDeepDive.UnitTests.IOC;
using MediatR;
using Ninject;
using Xunit;
using Xunit.Abstractions;
using Ninject.Extensions.Conventions;
using Ninject.Planning.Bindings.Resolvers;
using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.Repository;
using DtoDeepDive.Data.Service;
using DtoDeepDive.UnitTests;
using DtoDeepDive.UnitTests.PartCatalog;

namespace DtoDeepDive.IntegrationTests.PartCatalog {
    public class Part_dto_assembler {
        private readonly ITestOutputHelper _output;
        public Part_dto_assembler(ITestOutputHelper output) {
            _output = output;
        }
        [Fact]
        public void Assemble_part_dto_from_part() {
            var assembler = new PartAssembler();
            var part = Fixtures.BuildSinglePart();

            var partDto = assembler.WritePartDto(part);

            Assert.Equal(5, partDto.Components.Count());
            Assert.Equal(5, partDto.Labor.Count());
        }

        [Fact]
        public void Mediator_pattern_partdto_assembly() {
            using (var kernel = new StandardKernel()) {

                kernel.Components.Add<IBindingResolver, ContravariantBindingResolver>();
                kernel.Bind(scan => scan.FromAssemblyContaining<IMediator>()
                    .SelectAllClasses()
                    .BindDefaultInterface());
                kernel.Bind(scan => scan.FromAssemblyContaining<PartDTO.Query>()
                    .SelectAllClasses()
                    .BindAllInterfaces());

                kernel.Bind<SingleInstanceFactory>().ToMethod(ctx => t => ctx.Kernel.Get(t));
                kernel.Bind<MultiInstanceFactory>().ToMethod(ctx => t => ctx.Kernel.GetAll(t));

                var mediator = kernel.Get<IMediator>();
                var query = new PartDTO.Query() {
                    PartNumber = "TEST-PART-NUMBER|0"
                };
                var part = mediator.Send(query);
                _output.WriteLine(part.ToJson());
            }
        }
    }
}
