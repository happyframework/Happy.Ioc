using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc
{
    /// <summary>
    /// 标记为一个组件。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class ComponentAttribute : Attribute
    {
        /// <summary>
        /// 构造方法。
        /// </summary>
        public ComponentAttribute(Type[] services)
        {
            Check.MustNotNull(services, "services");

            this.LifeStyle = ComponentLifeStyle.Singleton;
        }

        /// <summary>
        /// 组件名称。
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 提供的服务。
        /// </summary>
        public Type[] Services { get; private set; }

        /// <summary>
        /// 生命周期风格。
        /// </summary>
        public ComponentLifeStyle LifeStyle { get; private set; }
    }
}
