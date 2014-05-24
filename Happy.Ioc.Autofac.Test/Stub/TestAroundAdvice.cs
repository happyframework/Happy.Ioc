using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Happy.Ioc.Aop;

namespace Happy.Ioc.Autofac.Test.Stub
{
    public sealed class TestAroundAdvice : IAroundAdvice
    {
        private readonly string _beforeMessage;
        private readonly string _afterMessage;

        public TestAroundAdvice(string beforeMessage, string afterMessage)
        {
            _beforeMessage = beforeMessage;
            _afterMessage = afterMessage;
        }

        public object Intercept(IMethodInvocation invocation)
        {
            MessageCenter.Message.Append(_beforeMessage);
            var result = invocation.Proceed();
            MessageCenter.Message.Append(_afterMessage);

            return result;
        }
    }
}
