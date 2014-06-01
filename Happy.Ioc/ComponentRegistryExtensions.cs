using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using Happy.Utils.Reflection;

namespace Happy.Ioc
{
    /// <summary>
    /// 扩展<see cref="IComponentRegistry"/>
    /// </summary>
    public static class ComponentRegistryExtensions
    {
        /// <summary>
        /// 注册实例。
        /// </summary>
        public static IComponentRegistry RegisterInstance(this IComponentRegistry registry,
                                                          object instance,
                                                          params Type[] services)
        {
            registry.RegisterInstance(string.Empty, instance, services);

            return registry;
        }

        /// <summary>
        /// 注册类型。
        /// </summary>
        public static IComponentRegistry Register(this IComponentRegistry registry,
                                                  Type componentType,
                                                  params Type[] services)
        {
            registry.Register(string.Empty, componentType, services,
                                                            ComponentLifeStyle.Singleton);

            return registry;
        }

        /// <summary>
        /// 注册类型。
        /// </summary>
        public static IComponentRegistry Register(this IComponentRegistry registry,
                                                  string name, Type componentType,
                                                  params Type[] services)
        {
            registry.Register(name, componentType, services,
                                                            ComponentLifeStyle.Singleton);

            return registry;
        }

        /// <summary>
        /// 注册类型。
        /// </summary>
        public static IComponentRegistry Register(this IComponentRegistry registry,
                                                  string name, Type componentType,
                                                  Type[] services,
                                                  ComponentLifeStyle lifeStyle)
        {
            registry.Register(name, componentType, services, lifeStyle,
                                                    Enumerable.Empty<DependencyInfo>());

            return registry;
        }

        /// <summary>
        /// 执行<paramref name="assemblies"/>中的所有<see cref="IComponentManualRegister"/>
        /// 类型实例。
        /// </summary>
        public static IComponentRegistry ManualRegister(this IComponentRegistry registry,
                                                        IEnumerable<Assembly> assemblies)
        {
            Check.MustNotNull(registry, "registry");
            Check.MustNotNull(assemblies, "assemblies");

            foreach (var assembly in assemblies)
            {
                registry.ManualRegister(assembly);
            }

            return registry;
        }

        /// <summary>
        /// 执行<paramref name="assembly"/>中的所有<see cref="IComponentManualRegister"/>
        /// 类型实例。
        /// </summary>
        public static IComponentRegistry ManualRegister(this IComponentRegistry registry,
                                                                    Assembly assembly)
        {
            Check.MustNotNull(registry, "registry");
            Check.MustNotNull(assembly, "assembly");

            var registers = assembly
                        .CreateConcreteDescendentInstances<IComponentManualRegister>();
            foreach (var register in registers)
            {
                register.Register(registry);
            }

            return registry;
        }

        /// <summary>
        /// 根据<see cref="ComponentAttribute"/>自动将<paramref name="assemblies"/>中的类型
        /// 注册到<paramref name="registry"/>中。
        /// </summary>
        public static IComponentRegistry AutoRegister(this IComponentRegistry registry,
                                                      IEnumerable<Assembly> assemblies)
        {
            Check.MustNotNull(registry, "registry");
            Check.MustNotNull(assemblies, "assemblies");

            foreach (var assembly in assemblies)
            {
                registry.AutoRegister(assembly);
            }

            return registry;
        }

        /// <summary>
        /// 根据<see cref="ComponentAttribute"/>自动将<paramref name="assembly"/>中的类型
        /// 注册到<paramref name="registry"/>中。
        /// </summary>
        public static IComponentRegistry AutoRegister(this IComponentRegistry registry,
                                                           Assembly assembly)
        {
            Check.MustNotNull(registry, "registry");
            Check.MustNotNull(assembly, "assembly");

            foreach (var type in assembly.GetTypes())
            {
                registry.AutoRegister(type);
            }

            return registry;
        }

        /// <summary>
        /// 根据<see cref="ComponentAttribute"/>自动将<paramref name="type"/>中的类型注册
        /// 到<paramref name="registry"/>中。
        /// </summary>
        public static IComponentRegistry AutoRegister(this IComponentRegistry registry,
                                                           Type type)
        {
            Check.MustNotNull(registry, "registry");
            Check.MustNotNull(type, "type");

            var componentAttribute = type.GetAttribute<ComponentAttribute>();
            if (componentAttribute == null)
            {
                return registry;
            }

            var dependencies = GetDependencies(type);

            registry.Register(name: componentAttribute.GetComponentName(type),
                              componentType: type,
                              services: componentAttribute.Services,
                              lifeStyle: componentAttribute.LifeStyle,
                              dependencies: dependencies);

            return registry;
        }

        private static IEnumerable<DependencyInfo> GetDependencies(Type type)
        {
            var constructor = type.GetConstructors()
                                  .OrderByDescending(x => x.GetParameters().Count())
                                  .First();

            return (from parameter in constructor.GetParameters()
                    let dependencyAttribute =
                                            parameter.GetAttribute<DependencyAttribute>()
                    where dependencyAttribute != null
                    select new DependencyInfo(parameter.Name, dependencyAttribute.Name))
                    .ToList();
        }
    }
}
