using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 基于委托的方法匹配器。
    /// </summary>
    public sealed class PredicateMethodMatcher : IMethodMatcher
    {
        private readonly Func<MethodInfo, Type, bool> _predicate;

        /// <summary>
        /// 构造方法。
        /// </summary>
        /// <param name="predicate"></param>
        public PredicateMethodMatcher(Func<MethodInfo, Type, bool> predicate)
        {
            Check.MustNotNull(predicate, "predicate");

            _predicate = predicate;
        }

        /// <inheritdoc />
        public bool IsRuntime
        {
            get { return false; }
        }

        /// <inheritdoc />
        public bool Matches(MethodInfo method, Type targetType)
        {
            return _predicate(method, targetType);
        }

        /// <inheritdoc />
        public bool Matches(MethodInfo method, Type targetType, object[] args)
        {
            return true;
        }
    }
}
