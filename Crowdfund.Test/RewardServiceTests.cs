using System;
using System.Linq;

using Xunit;
using Autofac;

using Crowdfund.Core.Services;
using Crowdfund.Core.Model.Options;

namespace Crowdfund.Test {
    public partial class RewardServiceTests : IClassFixture<CrowdfundFixture>
    {
        private readonly IRewardService rsvc_;
        private readonly IOwnerService osvc_;

        public RewardServiceTests(CrowdfundFixture fixture)
        {
            rsvc_ = fixture.Container.Resolve<IRewardService>();
            osvc_ = fixture.Container.Resolve<IOwnerService>();
        }


        [Fact]
        public void CreateRewardSuccess()
        {
            var option = new CreateRewardOptions()
            {
                Title = $"The First Reward test{DateTime.Now.Millisecond}",
                Description = $"This is the first reward you can buy" +
                                $"{DateTime.Now.Millisecond}",
                Value =  775.12M
            };

            var reward = rsvc_.CreateReward(2,option);
            var owner = osvc_.SearchOwnerById(2);

            var exist = owner.Rewards.Contains(reward);

            Assert.NotNull(reward);
            Assert.True(exist);
        }

    }
}
