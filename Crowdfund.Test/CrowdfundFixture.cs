using System;
using Autofac;
using Crowdfund.Core;
using Crowdfund.Core.Data;

namespace Crowdfund.Test {
    public class CrowdfundFixture : IDisposable 
    {

        public CrowdfundDbContext DbContext { get; private set; }
        public ILifetimeScope Container { get; private set; }

        public CrowdfundFixture()
        {
            Container = ServiceRegistrator.GetContainer().BeginLifetimeScope();
            DbContext = Container.Resolve<CrowdfundDbContext>();
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
