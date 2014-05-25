using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Happy.Ioc.Aop;

namespace Happy.Ioc.Autofac.Test.Stub
{
    public sealed class TestPointcutAspectAttribute : PointcutAspectAttribute
    {
        private readonly string _beforeMessage;
        private readonly string _afterMessage;

        public TestPointcutAspectAttribute(string beforeMessage, string afterMessage)
        {
            _beforeMessage = beforeMessage;
            _afterMessage = afterMessage;
        }

        public override IPointcutAdvice Advice
        {
            get { return new TestAroundAdvice(_beforeMessage, _afterMessage); }
        }
    }
}
