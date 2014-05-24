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
        public IntroductionAdvice(object instance)
            : this(new object[] { instance })
        {
        }

        /// <summary>
        /// 构造方法。
        /// </summary>
        public IntroductionAdvice(object[] instances)
        {
            Check.MustNotEmpty(instances, "instances");

            this.Instances = instances;
        }

        /// <inheritdoc />
        public object[] Instances { get; private set; }
    }
}
