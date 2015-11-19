using System.Linq;
using DtoDeepDive.Data.DTO;
using DtoDeepDive.Data.DTOAssemblers;
using DtoDeepDive.UnitTests.IOC;
using FluentAssertions;
using MediatR;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Planning.Bindings.Resolvers;
using Xunit;
using Xunit.Abstractions;

namespace DtoDeepDive.UnitTests.PartCatalog {
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
                kernel.Bind(scan => scan.FromAssemblyContaining<Query>()
                    .SelectAllClasses()
                    .BindAllInterfaces());

                kernel.Bind<SingleInstanceFactory>().ToMethod(ctx => t => ctx.Kernel.Get(t));
                kernel.Bind<MultiInstanceFactory>().ToMethod(ctx => t => ctx.Kernel.GetAll(t));

                var mediator = kernel.Get<IMediator>();
                var query = new Query() {
                    PartNumber = "TEST-PART-NUMBER|0"
                };
                var result = mediator.Send(query);

                result.PartDto.Should().BeOfType<PartDTO>();
            }
        }
    }
}
