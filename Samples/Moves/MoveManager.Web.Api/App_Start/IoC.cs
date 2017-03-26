using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.WebApi;
using MoveManager.Web.Api.Controllers;
using MoveManager.Web.Api.Services;
using SimpleCqrs.Implementation.AggregateStorages.SqlServer;
using SimpleCqrs.Implementation.AggregateStorages.SqlServer.Configuration;
using SimpleCqrs.Implementation.CommandProcessors;
using SimpleCqrs.Interface;

namespace MoveManager.Web.Api.App_Start
{
    public static class IoC
    {
        private static IDependencyResolver dependencyResolver;

        public static IDependencyResolver DependencyResolver
        {
            get
            {
                if (dependencyResolver == null)
                    dependencyResolver = new UnityHierarchicalDependencyResolver(UnityContainerRegisterTypes());
                    //dependencyResolver = new UnityDependencyResolver(UnityContainerRegisterTypes());

                return dependencyResolver;
            }
        }

        private static IUnityContainer UnityContainerRegisterTypes()
        {
            var container = new UnityContainer();

            //Controllers
            container.RegisterType<CallController>();

            //Services
            container.RegisterType<ICallService, CallService>(new HierarchicalLifetimeManager());

            //Cqrs
            container.RegisterType<ICommandProcessor, CommandProcessor>(new HierarchicalLifetimeManager());
            container.RegisterType<IEventStorage, SqlEventStorage>(new HierarchicalLifetimeManager());
            container.RegisterType<ISqlEventStorageConfiguration, SqlAggregateStorageConfiguration>(
                new HierarchicalLifetimeManager());

            return container;
        }
    }
}