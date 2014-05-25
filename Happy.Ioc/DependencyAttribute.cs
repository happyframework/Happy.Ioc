using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class DependencyAttribute : Attribute
    {
        /// <summary>
        /// 依赖的组件名称。
        /// </summary>
        public string Name { get; set; }
    }
}
