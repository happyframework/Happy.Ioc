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
        /// 创建<paramref name="target"/>的代理对象（类型为<paramref name="typeOfProxy"/>
        /// 且实现了<paramref name="additionalInterfacesOfProxy"/>接口列表）。
        /// </summary>
        object Create(object target, Type typeOfProxy, 
                                                    Type[] additionalInterfacesOfProxy);
    }
}
