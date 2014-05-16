using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc
{
    /// <summary>
    /// 组件生命周期风格。
    /// </summary>
    public enum ComponentLifeStyle
    {
        /// <summary>
        /// 单例。
        /// </summary>
        Singleton = 0,
        /// <summary>
        /// 透明，每次都创建新的实例。
        /// </summary>
        Transient = 1,
        /// <summary>
        /// 上下文单例，在Web环境即：请求级单例，在其它环境即：线程级单例。
        /// </summary>
        ContextSingleton = 2
    }
}
