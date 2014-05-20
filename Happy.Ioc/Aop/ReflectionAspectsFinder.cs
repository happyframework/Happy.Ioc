using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Happy.Utils;
using Happy.Utils.Reflection;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 基于反射的实现。
    /// </summary>
    public sealed class ReflectionAspectsFinder : IAspectsFinder
    {
        /// <inheritdoc />
        public IEnumerable<IAspect> FindAspects(Type type)
        {
            var globalAspects = AppDomain.CurrentDomain
                                         .CreateConcreteDescendentInstances<IAspect>();


            var typeAspects = (from item in type.GetAttributes<PointcutAttribute>(true)
                               let typeFilter = new PredicateTypeFilter(x => x == type)
                               let pointcut = new Pointcut(typeFilter)
                               select new PointcutAspect(pointcut, item.Advice));

            var mehodAspects = type.GetMethods().SelectMany(method =>
            {
                return (from item in method.GetAttributes<PointcutAttribute>(true)
                        let typeFilter = new PredicateTypeFilter(t => t == type)
                        let methodMatcher = new PredicateMethodMatcher((m, t) =>
                                                                            m == method)
                        let pointcut = new Pointcut(typeFilter, methodMatcher)
                        select new PointcutAspect(pointcut, item.Advice));
            });

            return globalAspects.Concat(typeAspects).Concat(mehodAspects).ToList();
        }
    }
}
