using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 引入增强。
    /// </summary>
    public interface IIntroductionAdvice : IAdvice
    {
        /// <summary>
        /// 引入（掺入）的类型列表。
        /// </summary>
        Type[] Types{ get; }
    }
}
