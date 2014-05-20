using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Castle.DynamicProxy;

namespace Happy.Ioc.Aop.Castle
{
    /// <summary>
    /// 基于Castle动态代理的实现。
    /// </summary>
    public sealed class CastleProxyFactory : IProxyFactory
    {
        private static ProxyGenerator _proxyGenerator = new ProxyGenerator();
        private readonly IAspectsFinder _aspectsFinder;

        /// <summary>
        /// 构造方法。
        /// </summary>
        public CastleProxyFactory()
            : this(new ReflectionAspectsFinder())
        {
        }

        /// <summary>
        /// 构造方法。
        /// </summary>
        public CastleProxyFactory(IAspectsFinder aspectsFinder)
        {
            Check.MustNotNull(aspectsFinder, "aspectsFinder");

            _aspectsFinder = aspectsFinder;
        }

        /// <inheritdoc />
        public object Create(object target, Type typeToProxy,
                                                Type[] additionalInterfacesToProxy)
        {
            var options = new ProxyGenerationOptions
            {
                Selector = new PointcutAspectInterceptorSelector()
            };

            if (typeToProxy.IsInterface)
            {
                return _proxyGenerator.CreateInterfaceProxyWithTarget(typeToProxy,
                                                      additionalInterfacesToProxy,
                                                      target, options,
                                                      GetInterceptors(target));
            }
            else
            {
                return _proxyGenerator.CreateClassProxyWithTarget(typeToProxy,
                                            additionalInterfacesToProxy,
                                            target, options, GetInterceptors(target));
            }
        }

        private IInterceptor[] GetInterceptors(object target)
        {
            return (from aspect in _aspectsFinder.FindAspects(target.GetType())
                    where aspect is IPointcutAspect
                          && (aspect as IPointcutAspect).Pointcut.TypeFilter
                                                            .Matches(target.GetType())
                    select new PointcutAspectInterceptor(aspect as IPointcutAspect))
                                                                            .ToArray();
        }

        private class PointcutAspectInterceptorSelector : IInterceptorSelector
        {
            public IInterceptor[] SelectInterceptors(Type type, MethodInfo method,
                                                            IInterceptor[] interceptors)
            {
                return (from interceptor in interceptors
                        let aspectInterceptor = interceptor as PointcutAspectInterceptor
                        where aspectInterceptor == null
                              || (aspectInterceptor.Aspect.Pointcut.MethodMatcher
                                                                .Matches(method, type))
                        select interceptor).ToArray();
            }
        }
    }
}
