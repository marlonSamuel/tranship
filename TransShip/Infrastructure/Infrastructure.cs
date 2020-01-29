using System;
using System.Collections.Generic;
using Ninject;
using Ninject.Syntax;
using System.Web.Mvc;
using TransShipModel.Abstract;
using TransShipModel.Concret;

namespace TransShip.Infrastructure
{
    public class Infrastructure : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public Infrastructure()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        private void AddBindings()
        {
            Bind<INSecurity>().To<INSecurityImplementation>();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        public IBindingToSyntax<T> Bind<T>()
        {
            return _kernel.Bind<T>();
        }

        public IKernel Kernel
        {
            get { return _kernel; }
        }
    }
}