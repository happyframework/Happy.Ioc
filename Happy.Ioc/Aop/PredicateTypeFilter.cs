using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 基于委托的类型过滤器。
    /// </summary>
    public sealed class PredicateTypeFilter : ITypeFilter
    {
        private readonly Predicate<Type> _predicate;

        /// <summary>
        /// 构造方法。
        /// </summary>
        /// <param name="predicate"></param>
        public PredicateTypeFilter(Predicate<Type> predicate)
        {
            Check.MustNotNull(predicate, "predicate");

            _predicate = predicate;
        }

        /// <inheritdoc />
        public bool Matches(Type type)
        {
            return _predicate(type);
        }
    }
}
