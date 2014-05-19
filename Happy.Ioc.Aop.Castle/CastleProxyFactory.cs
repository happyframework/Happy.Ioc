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
        private readonly IAspectRegistry _aspectRegistry;

        /// <summary>
        /// 构造方法。
        /// </summary>
        public CastleProxyFactory(IAspectRegistry aspectRegistry)
        {
            Check.MustNotNull(aspectRegistry, "aspectRegistry");

            _aspectRegistry = aspectRegistry;
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

        private IInterceptor[] GetInterceptors(object component)
        {
            return (from aspect in _aspectRegistry.Aspects
                    where aspect is IPointcutAspect
                          && (aspect as IPointcutAspect).Pointcut.TypeFilter
                                                            .Matches(component.GetType())
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
                        || (aspectInterceptor.Aspect.Pointcut.MethodMatcher.Matches(
                                                                        method, type))
                        select interceptor).ToArray();
            }
        }
    }
}
