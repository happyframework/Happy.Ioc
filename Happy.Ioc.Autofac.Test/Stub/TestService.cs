using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Happy.Ioc.Aop;

namespace Happy.Ioc.Autofac.Test.Stub
{
    [TestPointcut("-TestClass-Start-", "-TestClass-End-")]
    [Introduction(typeof(IPlayable))]
    public sealed class TestService : ITestService
    {
        [TestPointcut("-TestMethod-Start-", "-TestMethod-End-")]
        public void TestMethod()
        {
            MessageCenter.Message.Append("-TestMethod-");
        }
    }
}
