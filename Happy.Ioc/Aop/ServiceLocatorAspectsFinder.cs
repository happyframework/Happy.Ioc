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
        /// <inheritdoc />
        public IEnumerable<IAspect> FindAspects(Type type)
        {
            var globalAspects = ServiceLocator.Current.GetAllInstances<IAspect>();
            var pointcutAspects = this.FindPointcutAspects(type);
            var introductionAspects = this.FindIntroductionAspects(type);

            return globalAspects.Concat(pointcutAspects)
                                .Concat(introductionAspects).ToList();
        }

        private IEnumerable<IAspect> FindPointcutAspects(Type type)
        {

            var typeAspects = (from item in type.GetAttributes<PointcutAspectAttribute>(true)
                               let typeFilter = new PredicateTypeFilter(t => t == type)
                               let pointcut = new Pointcut(typeFilter)
                               select new PointcutAspect(pointcut, item.Advice));

            var mehodAspects = type.GetMethods().SelectMany(method =>
            {
                return (from item in method.GetAttributes<PointcutAspectAttribute>(true)
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
            return (from item in type.GetAttributes<IntroductionAspectAttribute>(true)
                    let typeFilter = new PredicateTypeFilter(t => t == type)
                    let advice = new IntroductionAdvice(item.Types)
                    select new IntroductionAspect(typeFilter, advice));
        }
    }
}
