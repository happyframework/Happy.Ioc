using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <inheritdoc />
    public sealed class Pointcut : IPointcut
    {
        /// <summary>
        /// 构造方法。
        /// </summary>
        public Pointcut(IMethodMatcher methodMatcher)
            : this(TrueTypeFilter.True, methodMatcher)
        {
        }

        /// <summary>
        /// 构造方法。
        /// </summary>
        public Pointcut(ITypeFilter typeFilter)
            : this(typeFilter, TrueMethodMatcher.True)
        {
        }

        /// <summary>
        /// 构造方法。
        /// </summary>
        public Pointcut(ITypeFilter typeFilter, IMethodMatcher methodMatcher)
        {
            Check.MustNotNull(typeFilter, "typeFilter");
            Check.MustNotNull(methodMatcher, "methodMatcher");

            this.TypeFilter = typeFilter;
            this.MethodMatcher = methodMatcher;
        }

        /// <inheritdoc />
        public ITypeFilter TypeFilter { get; private set; }

        /// <inheritdoc />
        public IMethodMatcher MethodMatcher { get; private set; }
    }
}
