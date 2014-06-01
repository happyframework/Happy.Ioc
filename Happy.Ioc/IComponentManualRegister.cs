using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc
{
    /// <summary>
    /// 组件手工登记处，用于手工登记，推荐使用<see cref="ComponentAttribute"/>。
    /// </summary>
    public interface IComponentManualRegister
    {
        /// <summary>
        /// 注册组件到<paramref name="registry"/>中。
        /// </summary>
        void Register(IComponentRegistry registry);
    }
}
