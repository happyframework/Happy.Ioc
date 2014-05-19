using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 切入点方面。
    /// </summary>
    public interface IPointcutAspect : IAspect
    {
        /// <summary>
        /// 切入点。
        /// </summary>
        IPointcut Pointcut { get; }

        /// <summary>
        /// 切入点增强。
        /// </summary>
        IPointcutAdvice Advice { get; }
    }
}
