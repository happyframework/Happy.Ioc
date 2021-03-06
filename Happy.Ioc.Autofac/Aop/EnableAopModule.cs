﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autofac;
using Autofac.Core;

using Happy.Ioc.Aop;

namespace Happy.Ioc.Autofac.Aop
{
    /// <summary>
    /// 启用AOP。
    /// </summary>
    public sealed class EnableAopModule : Module
    {
        private readonly IProxyFactory _proxyFactory;

        /// <summary>
        /// 构造方法。
        /// </summary>
        /// <param name="proxyFactory"></param>
        public EnableAopModule(IProxyFactory proxyFactory)
        {
            Check.MustNotNull(proxyFactory, "proxyFactory");

            _proxyFactory = proxyFactory;
        }

        /// <summary>
        /// 挂钩事件到<paramref name="registration"/>。
        /// </summary>
        protected override void AttachToComponentRegistration(
                                global::Autofac.Core.IComponentRegistry componentRegistry,
                                IComponentRegistration registration)
        {
            registration.Activating += OnActivating;
        }

        private void OnActivating(object sender, ActivatingEventArgs<object> e)
        {
            if (e.Instance.GetType().IsArray)
            {
                return;
            }

            var serviceWithTypes = e.Component.Services.OfType<IServiceWithType>()
                                              .Select(x => x.ServiceType).ToList();
            if (!serviceWithTypes.Any())
            {
                return;
            }

            if (serviceWithTypes.Count == 1)
            {
                e.Instance = _proxyFactory.Create(e.Instance, serviceWithTypes.First(),
                                                                    new Type[] { });
            }
            else if (serviceWithTypes.All(x => x.IsInterface))
            {
                e.Instance = _proxyFactory.Create(e.Instance, e.Instance.GetType(),
                                                        serviceWithTypes.ToArray());
            }
            else
            {
                throw new NotSupportedException();
            }
        }
    }
}
