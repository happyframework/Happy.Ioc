using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 代理工厂。
    /// </summary>
    public interface IProxyFactory
    {
        /// <summary>
        /// 创建代理对象。
        /// </summary>
        object Create(object target, Type typeToProxy,
                                                Type[] additionalInterfacesToProxy);
    }
}
