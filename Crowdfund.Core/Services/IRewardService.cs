using System;
using System.Text;
using Crowdfund.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Crowdfund.Core.Services {
    public interface IRewardService 
    {
        Task<ApiResult<Reward>> CreateRewardAsync(int ownerId,
            int projectId, Model.Options.CreateRewardOptions options);

        Task<ApiResult<List<Reward>>> SearchRewardByProjectIdAsync(int projectId);

        Task<ApiResult<Reward>> SearchRewardByIdAsync(int rewardId);
    }
}
