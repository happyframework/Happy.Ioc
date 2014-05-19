using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 方面注册表。
    /// </summary>
    public interface IAspectRegistry
    {
        /// <summary>
        /// 方面列表。
        /// </summary>
        IList<IAspect> Aspects { get; }
    }
}
