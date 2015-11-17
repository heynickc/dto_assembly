using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Reflection;
using DtoDeepDive.Data.DAL;
using DtoDeepDive.Data.DTO;
using DtoDeepDive.Data.Repository;
using DtoDeepDive.Data.Service;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Planning.Bindings.Resolvers;
using Ninject.Components;
using Ninject.Infrastructure;
using Ninject.Modules;
using Ninject.Planning.Bindings;
using MediatR;

namespace DtoDeepDive.UnitTests.IOC {
    public class ServiceModule : NinjectModule {
        public override void Load() {
            //Bind<IPartsCatalogDbContext>().To<PartsCatalogDbContext>();
            //Bind<IRepository<Part>>().To<PartRepository>();
            //Bind<IPartCatalogService>().To<PartCatalogService>();

            this.Kernel.Components.Add<IBindingResolver, ContravariantBindingResolver>();
            this.Bind(scan => scan.FromAssemblyContaining<IMediator>().SelectAllClasses().BindDefaultInterface());
            this.Bind(scan => scan.FromAssemblyContaining<Query>().SelectAllClasses().BindAllInterfaces());
            Bind<SingleInstanceFactory>().ToMethod(ctx => t => ctx.Kernel.Get(t));
            Bind<MultiInstanceFactory>().ToMethod(ctx => t => ctx.Kernel.GetAll(t));
        }
    }
    public class ContravariantBindingResolver : NinjectComponent, IBindingResolver {
        /// <summary>
        /// Returns any bindings from the specified collection that match the specified service.
        /// </summary>
        public IEnumerable<IBinding> Resolve(Multimap<Type, IBinding> bindings, Type service) {
            if (service.IsGenericType) {
                var genericType = service.GetGenericTypeDefinition();
                var genericArguments = genericType.GetGenericArguments();
                if (genericArguments.Count() == 1
                 && genericArguments.Single().GenericParameterAttributes.HasFlag(GenericParameterAttributes.Contravariant)) {
                    var argument = service.GetGenericArguments().Single();
                    var matches = bindings.Where(kvp => kvp.Key.IsGenericType
                                                           && kvp.Key.GetGenericTypeDefinition().Equals(genericType)
                                                           && kvp.Key.GetGenericArguments().Single() != argument
                                                           && kvp.Key.GetGenericArguments().Single().IsAssignableFrom(argument))
                        .SelectMany(kvp => kvp.Value);
                    return matches;
                }
            }

            return Enumerable.Empty<IBinding>();
        }
    }
}