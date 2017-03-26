using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Microsoft.Practices.Unity;

namespace MoveManager.Web.Api.App_Start
{
    public class UnityDependencyResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public UnityDependencyResolver(IUnityContainer container)
        {
            if (container == null) throw new ArgumentNullException(nameof(container));

            this.container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return this.container.Resolve(serviceType);
            }
            catch (ResolutionFailedException ex)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException ex)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = this.container.CreateChildContainer();

            return new UnityDependencyResolver(child);
        }

        public void Dispose()
        {
            this.container.Dispose();
        }
    }
}