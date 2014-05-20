using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <inheritdoc />
    public sealed class PointcutAspect : IPointcutAspect
    {
        /// <summary>
        /// 构造方法。
        /// </summary>
        public PointcutAspect(IPointcutAdvice advice)
            : this(TruePointcut.True, advice)
        {
        }

        /// <summary>
        /// 构造方法。
        /// </summary>
        public PointcutAspect(IPointcut pointcut, IPointcutAdvice advice)
        {
            Check.MustNotNull(pointcut, "pointcut");
            Check.MustNotNull(advice, "advice");

            this.Pointcut = pointcut;
            this.Advice = advice;
        }

        /// <inheritdoc />
        public IPointcut Pointcut { get; private set; }

        /// <inheritdoc />
        public IPointcutAdvice Advice { get; private set; }
    }
}
