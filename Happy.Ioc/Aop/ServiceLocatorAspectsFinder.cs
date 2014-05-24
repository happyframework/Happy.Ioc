using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Microsoft.Practices.ServiceLocation;

using Happy.Utils;
using Happy.Utils.Reflection;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 基于服务定位器的实现。
    /// </summary>
    public sealed class ServiceLocatorAspectsFinder : IAspectsFinder
    {
        private Lazy<IServiceLocator> _locator;

        /// <summary>
        /// 构造方法。
        /// </summary>
        public ServiceLocatorAspectsFinder()
        {
            _locator = new Lazy<IServiceLocator>(() => ServiceLocator.Current);
        }

        /// <summary>
        /// 构造方法。
        /// </summary>
        public ServiceLocatorAspectsFinder(IServiceLocator locator)
        {
            Check.MustNotNull(locator, "locator");

            _locator = new Lazy<IServiceLocator>(() => locator);
        }

        /// <inheritdoc />
        public IEnumerable<IAspect> FindAspects(Type type)
        {
            var globalAspects = _locator.Value.GetAllInstances<IAspect>();
            var pointcutAspects = this.FindPointcutAspects(type);
            var introductionAspects = this.FindIntroductionAspects(type);

            return globalAspects.Concat(pointcutAspects)
                                .Concat(introductionAspects).ToList();
        }

        private IEnumerable<IAspect> FindPointcutAspects(Type type)
        {

            var typeAspects = (from item in type.GetAttributes<PointcutAttribute>(true)
                               let typeFilter = new PredicateTypeFilter(t => t == type)
                               let pointcut = new Pointcut(typeFilter)
                               select new PointcutAspect(pointcut, item.Advice));

            var mehodAspects = type.GetMethods().SelectMany(method =>
            {
                return (from item in method.GetAttributes<PointcutAttribute>(true)
                        let typeFilter = new PredicateTypeFilter(t => t == type)
                        let methodMatcher = new PredicateMethodMatcher((m, t) =>
                                                                        m.Match(method))
                        let pointcut = new Pointcut(typeFilter, methodMatcher)
                        select new PointcutAspect(pointcut, item.Advice));
            });

            return typeAspects.Concat(mehodAspects);
        }

        private IEnumerable<IIntroductionAspect> FindIntroductionAspects(Type type)
        {
            return (from item in type.GetAttributes<IntroductionAttribute>(true)
                    let typeFilter = new PredicateTypeFilter(t => t == type)
                    let instances = item.Types
                                        .Select(x => _locator.Value.GetInstance(x))
                                        .ToArray()
                    let advice = new IntroductionAdvice(instances)
                    select new IntroductionAspect(typeFilter, advice));
        }
    }
}
