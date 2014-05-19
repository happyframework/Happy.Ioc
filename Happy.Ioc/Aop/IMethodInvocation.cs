using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 方法调用。
    /// </summary>
    public interface IMethodInvocation
    {
        /// <summary>
        /// 目标方法。
        /// </summary>
        MethodInfo Method { get; }

        /// <summary>
        /// 方法参数。
        /// </summary>
        object[] Arguments { get; }

        /// <summary>
        /// 代理对象。
        /// </summary>
        object Proxy { get; }

        /// <summary>
        /// 目标对象。
        /// </summary>
        object Target { get; }

        /// <summary>
        /// 目标方法所在的类型。
        /// </summary>
        Type TargetType { get; }

        /// <summary>
        /// 执行下一个增强。
        /// </summary>
        object Proceed();
    }
}
