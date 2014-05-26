using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 方法匹配器。
    /// </summary>
    public interface IMethodMatcher
    {
        /// <summary>
        /// 是否是运行时匹配。
        /// </summary>
        bool IsRuntime { get; }

        /// <summary>
        /// 精态匹配：<paramref name="targetType"/>的方法<paramref name="method"/>是否匹配。
        /// </summary>
        bool Matches(MethodInfo method, Type targetType);

        /// <summary>
        /// 动态匹配：<paramref name="targetType"/>的方法<paramref name="method"/>
        /// 在某次调用下（调用参数为：<paramref name="args"/>）是否匹配。
        /// </summary>
        bool Matches(MethodInfo method, Type targetType, object[] args);
    }
}
