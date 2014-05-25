using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 引入方面属性。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class IntroductionAspectAttribute : Attribute
    {
        /// <summary>
        /// 构造方法。
        /// </summary>
        public IntroductionAspectAttribute(Type type)
            : this(new Type[] { type })
        {
        }

        /// <summary>
        /// 构造方法。
        /// </summary>
        public IntroductionAspectAttribute(Type[] types)
        {
            Check.MustNotEmpty(types, "types");

            this.Types = types;
        }

        /// <summary>
        /// 引入（掺入）的类型列表。
        /// </summary>
        public Type[] Types { get; private set; }
    }
}
