using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Happy.Ioc.Aop;

namespace Happy.Ioc.Autofac.Test.Stub
{
    [TestPointcutAspect("-TestClass-Start-", "-TestClass-End-")]
    [IntroductionAspect(typeof(IPlayable))]
    public sealed class TestService : ITestService
    {
        [TestPointcutAspect("-TestMethod-Start-", "-TestMethod-End-")]
        public void TestMethod()
        {
            MessageCenter.Message.Append("-TestMethod-");
        }
    }
}
