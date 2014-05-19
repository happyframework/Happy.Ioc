using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Castle.DynamicProxy;

namespace Happy.Ioc.Aop.Castle
{
    /// <summary>
    /// 切入点方面拦截器。
    /// </summary>
    internal sealed class PointcutAspectInterceptor : IInterceptor
    {
        internal PointcutAspectInterceptor(IPointcutAspect aspect)
        {
            this.Aspect = aspect;
        }

        public IPointcutAspect Aspect { get; private set; }

        public void Intercept(IInvocation invocation)
        {
            var methodMatcher = this.Aspect.Pointcut.MethodMatcher;

            if (this.Aspect.Advice is IAroundAdvice)
            {
                var aroundAdvice = this.Aspect.Advice as IAroundAdvice;
                if ((methodMatcher.IsRuntime
                    && methodMatcher.Matches(invocation.Method, invocation.TargetType,
                                                                    invocation.Arguments))
                    || (!methodMatcher.IsRuntime))
                {
                    aroundAdvice.Intercept(new MethodInvocation(invocation));
                }
            }
        }

        private class MethodInvocation : IMethodInvocation
        {
            private readonly IInvocation _invocation;

            public MethodInvocation(IInvocation invocation)
            {
                _invocation = invocation;
            }

            public MethodInfo Method
            {
                get { return _invocation.Method; }
            }

            public object[] Arguments
            {
                get { return _invocation.Arguments; }
            }

            public object Proxy
            {
                get { return _invocation.Proxy; }
            }

            public object Target
            {
                get { return _invocation.InvocationTarget; }
            }

            public Type TargetType
            {
                get { return _invocation.TargetType; }
            }

            public object Proceed()
            {
                _invocation.Proceed();

                return _invocation.ReturnValue;
            }
        }
    }
}
