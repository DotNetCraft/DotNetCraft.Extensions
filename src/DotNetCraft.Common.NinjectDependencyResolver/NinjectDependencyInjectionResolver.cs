using System;
using System.Collections.Generic;
using DotNetCraft.Common.Utils.DependencyInjection;
using Ninject;

namespace DotNetCraft.Common.NinjectDependencyResolver
{
    public class NinjectDependencyInjectionResolver: BaseDependencyResolver
    {
        /// <summary>
        /// The kernel.
        /// </summary>
        private readonly IKernel kernel;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        /// <exception cref="ArgumentNullException"><paramref name="kernel"/> is <see langword="null"/></exception>
        public NinjectDependencyInjectionResolver(IKernel kernel)
        {
            if (kernel == null)
                throw new ArgumentNullException(nameof(kernel));

            this.kernel = kernel;
        }

        #region Overrides of BaseDependencyResolver

        protected override object OnResolve(Type serviceType, string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
                return kernel.Get(serviceType);

            return kernel.Get(serviceType, serviceName);
        }

        protected override IEnumerable<object> OnResolveAll(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        #endregion
    }
}
