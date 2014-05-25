using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc
{
    /// <summary>
    /// 依赖信息。
    /// </summary>
    public sealed class DependencyInfo
    {
        /// <summary>
        /// 构造方法。
        /// </summary>
        public DependencyInfo(string parameterName, string dependencyName)
        {
            Check.MustNotNull(parameterName, "parameterName");
            Check.MustNotNull(dependencyName, "dependencyName");

            this.ParameterName = parameterName;
            this.DependencyName = dependencyName;
        }

        /// <summary>
        /// 参数名称。
        /// </summary>
        public string ParameterName { get; private set; }

        /// <summary>
        /// 依赖的组件名称。
        /// </summary>
        public string DependencyName { get; private set; }
    }
}
