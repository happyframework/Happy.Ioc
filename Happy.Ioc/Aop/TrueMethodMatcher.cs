using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 典型类型。
    /// </summary>
    public sealed class TrueMethodMatcher : IMethodMatcher
    {
        public static readonly IMethodMatcher True = 
                                                new TrueMethodMatcher();

        /// <inheritdoc />
        public bool IsRuntime
        {
            get { return false; }
        }

        /// <inheritdoc />
        public bool Matches(MethodInfo method, Type targetType)
        {
            return true;
        }

        /// <inheritdoc />
        public bool Matches(MethodInfo method, Type targetType,
                                                            object[] args)
        {
            return true;
        }
    }
}
