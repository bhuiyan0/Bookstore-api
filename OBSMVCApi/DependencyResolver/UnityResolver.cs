﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Unity;

namespace OBSMVCApi.DependencyResolver
{
    public class UnityResolver:IDependencyResolver
    {
        protected IUnityContainer container;
        public UnityResolver(IUnityContainer unityContainer)
        {
            container = unityContainer;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
        protected virtual void Dispose(bool disposing)
        {
            container.Dispose();
        }
    }
}