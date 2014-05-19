using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 引入方面。
    /// </summary>
    public interface IIntroductionAspect : IAspect
    {
        /// <summary>
        /// 类型过滤器。
        /// </summary>
        ITypeFilter TypeFilter { get; }

        /// <summary>
        /// 引入增强。
        /// </summary>
        IIntroductionAdvice Advice { get; }
    }
}
