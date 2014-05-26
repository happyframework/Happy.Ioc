using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc
{
    /// <summary>
    /// 组件注册表。
    /// </summary>
    public interface IComponentRegistry
    {
        /// <summary>
        /// 注册类型。
        /// </summary>
        IComponentRegistry Register(string name, Type componentType, Type[] services,
                                    ComponentLifeStyle lifeStyle,
                                    IEnumerable<DependencyInfo> dependencies);

        /// <summary>
        /// 注册实例。
        /// </summary>
        IComponentRegistry RegisterInstance(string name, object instance, Type[] services);
    }
}
