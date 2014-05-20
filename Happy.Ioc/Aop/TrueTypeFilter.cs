using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 典型类型。
    /// </summary>
    public sealed class TrueTypeFilter : ITypeFilter
    {
        public static readonly ITypeFilter True = new TrueTypeFilter();
        
        private TrueTypeFilter() { }

        /// <inheritdoc />
        public bool Matches(Type type)
        {
            return true;
        }
    }
}
