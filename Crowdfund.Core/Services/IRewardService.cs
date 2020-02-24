using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Crowdfund.Core.Model;

namespace Crowdfund.Core.Services {
    public interface IRewardService 
    {
        Task<ApiResult<Reward>> CreateRewardAsync(int ownerId,
            int projectId, Model.Options.CreateRewardOptions options);

        Task<ApiResult<List<Reward>>> SearchRewardByProjectIdAsync(int projectId);

        Task<ApiResult<Reward>> SearchRewardByIdAsync(int rewardId);
    }
}
