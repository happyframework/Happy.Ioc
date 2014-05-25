using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 切点方面属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public abstract class PointcutAspectAttribute : Attribute
    {
        /// <summary>
        /// 切点增强。
        /// </summary>
        public abstract IPointcutAdvice Advice { get; }
    }
}
