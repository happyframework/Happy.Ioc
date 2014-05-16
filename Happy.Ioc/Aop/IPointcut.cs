using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 连接点。
    /// </summary>
    public interface IPointcut
    {
        /// <summary>
        /// 类型过滤器。
        /// </summary>
        ITypeFilter TypeFilter { get; }

        /// <summary>
        /// 方法匹配器。
        /// </summary>
        IMethodMatcher MethodMatcher { get; }
    }
}
