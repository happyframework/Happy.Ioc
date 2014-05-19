using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Happy.Utils;

namespace Happy.Ioc.Aop
{
    /// <summary>
    /// 从当前的AppDomain返回所有的方面。
    /// </summary>
    public sealed class AppDomainAspectRegistry : IAspectRegistry
    {
        public IList<IAspect> Aspects
        {
            get
            {
                return AppDomain.CurrentDomain
                                            .CreateConcreteDescendentInstances<IAspect>()
                                            .ToList();
            }
        }
    }
}
