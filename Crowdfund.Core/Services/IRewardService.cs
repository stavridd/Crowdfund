using System;
using System.Collections.Generic;
using System.Text;
using Crowdfund.Core.Model;

namespace Crowdfund.Core.Services {
    public interface IRewardService 
    {
        Reward CreateReward(int ownerId,
            Model.Options.CreateRewardOptions options);

        Reward SearchRewardById(int rewardId);
    }
}
