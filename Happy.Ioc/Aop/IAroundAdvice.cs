using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 环绕增强。
    /// </summary>
    public interface IAroundAdvice : IPointcutAdvice
    {
        /// <summary>
        /// 拦截<paramref name="invocation"/>代表的方法调用。
        /// </summary>
        object Intercept(IMethodInvocation invocation);
    }
}
