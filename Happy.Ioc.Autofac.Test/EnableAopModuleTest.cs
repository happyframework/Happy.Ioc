using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Autofac;
using Autofac.Core;
using Microsoft.Practices.ServiceLocation;

using Happy.Ioc.Castle.Aop;
using Happy.Ioc.Autofac.Aop;
using Happy.Ioc.Autofac.ServiceLocation;
using Happy.Ioc.Autofac.Test.Stub;

namespace Happy.Ioc.Autofac.Test
{
    [TestFixture]
    public class EnableAopModuleTest
    {
        [Test]
        public void Aop_Test()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new EnableAopModule(new CastleProxyFactory()));
            builder.RegisterType<Playable>()
                   .As<IPlayable>();
            builder.RegisterType<TestService>()
                   .As<ITestService>();
            var container = builder.Build();

            ServiceLocator.SetLocatorProvider(() =>
                                                new AutofacServiceLocator(container));

            var service = container.Resolve<ITestService>();
            MessageCenter.Message.Clear();
            service.TestMethod();

            Assert.AreEqual(
                "-TestClass-Start-"
                + "-TestMethod-Start-"
                + "-TestMethod-"
                + "-TestMethod-End-"
                + "-TestClass-End-", MessageCenter.Message.ToString());


            Assert.AreEqual("Play", (service as IPlayable).Play());
        }
    }
}
