using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Happy.Ioc
{
    public interface IComponentRegistry
    {
        IComponentRegistry Register(string name, Type componentType, Type[] services,
                                    ComponentLifeStyle componentLifeStyle,
                                    IEnumerable<DependencyInfo> dependencies);

        IComponentRegistry RegisterInstance(string name, object instance, Type[] services,
                                    ComponentLifeStyle componentLifeStyle,
                                    IEnumerable<DependencyInfo> dependencies);
    }
}
