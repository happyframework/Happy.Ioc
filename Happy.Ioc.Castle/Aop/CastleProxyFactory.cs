using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Castle.DynamicProxy;
using Microsoft.Practices.ServiceLocation;

using Happy.Ioc.Aop;

namespace Happy.Ioc.Castle.Aop
{
    /// <summary>
    /// 基于Castle动态代理的实现。
    /// </summary>
    public sealed class CastleProxyFactory : IProxyFactory
    {
        private static ProxyGenerator _proxyGenerator = new ProxyGenerator();
        private readonly IAspectsFinder _aspectsFinder;
        private readonly Lazy<IServiceLocator> _locator =
                                new Lazy<IServiceLocator>(() => ServiceLocator.Current);

        /// <summary>
        /// 构造方法。
        /// </summary>
        public CastleProxyFactory()
            : this(new ServiceLocatorAspectsFinder())
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

        /// <summary>
        /// 构造方法。
        /// </summary>
        public CastleProxyFactory(IAspectsFinder aspectsFinder, IServiceLocator locator)
        {
            Check.MustNotNull(aspectsFinder, "aspectsFinder");
            Check.MustNotNull(locator, "locator");

            _aspectsFinder = aspectsFinder;
            _locator = new Lazy<IServiceLocator>(() => locator);
        }

        /// <inheritdoc />
        public object Create(object target, Type typeToProxy,
                                            Type[] additionalInterfacesToProxy = null)
        {
            Check.MustNotNull("target", "target");
            Check.MustNotNull("typeToProxy", "typeToProxy");

            var aspects = _aspectsFinder.FindAspects(target.GetType());
            if (!aspects.Any())
            {
                return target;
            }

            var options = new ProxyGenerationOptions
            {
                Selector = new PointcutAspectInterceptorSelector()
            };
            foreach (var instance in this.GetMixinInstances(target.GetType(), aspects))
            {
                options.AddMixinInstance(instance);
            }

            var interceptors = this.GetInterceptors(target.GetType(), aspects);
            if (typeToProxy.IsInterface)
            {
                return _proxyGenerator.CreateInterfaceProxyWithTarget(typeToProxy,
                                                       additionalInterfacesToProxy,
                                                       target, options, interceptors);
            }
            else
            {
                return _proxyGenerator.CreateClassProxyWithTarget(typeToProxy,
                                                       additionalInterfacesToProxy,
                                                       target, options, interceptors);
            }
        }

        private IEnumerable<object> GetMixinInstances(Type type,
                                                            IEnumerable<IAspect> aspects)
        {
            return (from aspect in aspects
                    let introductionAspect = aspect as IIntroductionAspect
                    where introductionAspect != null
                          && introductionAspect.TypeFilter.Matches(type)
                    from t in introductionAspect.Advice.Types
                    select _locator.Value.GetInstance(t));
        }

        private IInterceptor[] GetInterceptors(Type type, IEnumerable<IAspect> aspects)
        {
            return (from aspect in aspects
                    let pointcutAspect = aspect as IPointcutAspect
                    where pointcutAspect != null
                          && pointcutAspect.Pointcut.TypeFilter
                                                            .Matches(type)
                    select new PointcutAspectInterceptor(pointcutAspect)).ToArray();
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
