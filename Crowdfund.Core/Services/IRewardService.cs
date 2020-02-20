using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Crowdfund.Core.Model;

namespace Crowdfund.Core.Services {
    public interface IRewardService 
    {
        Task<ApiResult<Reward>> CreateRewardAsync(int ownerId,
            Model.Options.CreateRewardOptions options);

        Task<ApiResult<Reward>> SearchRewardByIdSAsync(int rewardId);
    }
}
