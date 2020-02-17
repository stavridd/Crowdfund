using System;
using Autofac;

using Crowdfund.Core.Services;

namespace Crowdfund.Core {
    public class ServiceRegistrator 
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            builder
                .RegisterType<OwnerService>()
                .InstancePerLifetimeScope()
                .As<IOwnerService>();

            builder
                .RegisterType<BuyerService>()
                .InstancePerLifetimeScope()
                .As<IBuyerService>();

            builder
                .RegisterType<ProjectService>()
                .InstancePerLifetimeScope()
                .As<IProjectService>();

            builder
                .RegisterType<RewardService>()
                .InstancePerLifetimeScope()
                .As<IRewardService>();

            builder
                .RegisterType<Data.CrowdfundDbContext>()
                .InstancePerLifetimeScope()
                .AsSelf();

            return builder.Build();
        }
    }
}
