using System;
using Autofac;
using Crowdfund.Core.Services;

namespace Crowdfund.Core {
    public class ServiceRegistrator : Module
    {
        public static void RegisterServices(ContainerBuilder builder)
        {
            if (builder == null) {
                throw new ArgumentNullException(nameof(builder));
            }

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

            builder
                .RegisterType<LoggerService>()
                .InstancePerLifetimeScope()
                .As<ILoggerService>();
        }

        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();

            RegisterServices(builder);

            return builder.Build();
        }

        protected override void Load(ContainerBuilder builder)
        {
            RegisterServices(builder);
        }

       

        
    }
}
