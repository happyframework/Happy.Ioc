using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 方面查询器。
    /// </summary>
    public interface IAspectsFinder
    {
        /// <summary>
        /// 查询方面列表。
        /// </summary>
        IEnumerable<IAspect> FindAspects(Type type);
    }
}
