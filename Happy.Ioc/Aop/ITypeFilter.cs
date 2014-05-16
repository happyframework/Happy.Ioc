using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 类型过滤器。
    /// </remarks>
    public interface ITypeFilter
    {
        /// <summary>
        /// <paramref name="type"/>是否满足条件。
        /// </summary>
        bool Matches(Type type);
    }
}
