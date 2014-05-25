using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <inheritdoc />
    public sealed class IntroductionAdvice : IIntroductionAdvice
    {
        /// <summary>
        /// 构造方法。
        /// </summary>
        public IntroductionAdvice(Type type)
            : this(new Type[] { type })
        {
        }

        /// <summary>
        /// 构造方法。
        /// </summary>
        public IntroductionAdvice(Type[] types)
        {
            Check.MustNotEmpty(types, "types");

            this.Types = types;
        }

        /// <inheritdoc />
        public Type[] Types { get; private set; }
    }
}
