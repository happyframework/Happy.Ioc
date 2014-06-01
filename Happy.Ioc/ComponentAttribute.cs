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
        public ComponentAttribute(Type service)
            : this(new Type[] { service })
        {
        }

        /// <summary>
        /// 构造方法。
        /// </summary>
        public ComponentAttribute(Type[] services)
            : this(string.Empty, services)
        {
        }

        /// <summary>
        /// 构造方法。
        /// </summary>
        public ComponentAttribute(string name, Type[] services)
        {
            Check.MustNotNull(name, "name");
            Check.MustNotNull(services, "services");

            this.Name = name;
            this.LifeStyle = ComponentLifeStyle.Singleton;
        }

        /// <summary>
        /// 自动命名组件。
        /// </summary>
        public bool AutoNamed { get; set; }

        /// <summary>
        /// 组件名称。
        /// </summary>
        private string Name { get; set; }

        /// <summary>
        /// 提供的服务。
        /// </summary>
        public Type[] Services { get; private set; }

        /// <summary>
        /// 生命周期风格。
        /// </summary>
        public ComponentLifeStyle LifeStyle { get; set; }

        /// <summary>
        /// 获取组件的名称。
        /// </summary>
        public string GetComponentName(Type componentType)
        {
            Check.MustNotNull(componentType, "componentType");

            return this.AutoNamed ? componentType.FullName : this.Name;
        }
    }
}
