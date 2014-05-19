using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 典型类型。
    /// </summary>
    public sealed class TruePointcut : IPointcut
    {
        public static readonly TruePointcut True =
                                                new TruePointcut();

        /// <inheritdoc />
        public ITypeFilter TypeFilter
        {
            get { return TrueTypeFilter.True; }
        }

        /// <inheritdoc />
        public IMethodMatcher MethodMatcher
        {
            get { return TrueMethodMatcher.True; }
        }
    }
}
