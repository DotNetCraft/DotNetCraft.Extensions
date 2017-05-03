using DotNetCraft.Common.NinjectDependencyResolver;
using Ninject;
using NUnit.Framework;

namespace NinjectDependencyResolver.Tests
{
    [TestFixture]
    public class NinjectDependencyResolverTests
    {
        interface ISimpleInterface
        {
            string Property { get; }
        }

        class SimpleInterface: ISimpleInterface
        {
            #region Implementation of ISimpleInterface

            public string Property
            {
                get
                {
                    return "Test";
                }
            }

            #endregion
        }

        [Test]
        public void SimpleResolveTest()
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<ISimpleInterface>().To<SimpleInterface>();

            NinjectDependencyInjectionResolver resolver = new NinjectDependencyInjectionResolver(kernel);
            ISimpleInterface instance = resolver.Resolve<ISimpleInterface>();
            Assert.IsTrue(instance.GetType() == typeof(SimpleInterface));

            Assert.AreEqual("Test", instance.Property);
        }
    }
}
