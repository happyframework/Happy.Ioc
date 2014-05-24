using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <inheritdoc />
    public class IntroductionAspect : IIntroductionAspect
    {
        /// <summary>
        /// 构造方法。
        /// </summary>
        public IntroductionAspect(IIntroductionAdvice advice)
            : this(TrueTypeFilter.True, advice)
        {
        }

        /// <summary>
        /// 构造方法。
        /// </summary>
        public IntroductionAspect(ITypeFilter typeFilter, IIntroductionAdvice advice)
        {
            Check.MustNotNull(typeFilter, "typeFilter");
            Check.MustNotNull(advice, "advice");

            this.TypeFilter = typeFilter;
            this.Advice = advice;
        }

        /// <inheritdoc />
        public ITypeFilter TypeFilter { get; private set; }

        /// <inheritdoc />
        public IIntroductionAdvice Advice { get; private set; }
    }
}
