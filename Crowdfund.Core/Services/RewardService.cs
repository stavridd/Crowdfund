using System;
using System.Linq;
using System.Text;
using Crowdfund.Core;
using Crowdfund.Core.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Crowdfund.Core.Services {
    public class RewardService : IRewardService 
    {
        private readonly Data.CrowdfundDbContext context_;
        private readonly IOwnerService owners_;
        private readonly ILoggerService logger_;

        public RewardService(Data.CrowdfundDbContext context,
            IOwnerService owners, ILoggerService logger)
        {
            context_ = context ?? throw new ArgumentException(nameof(context));
            owners_ = owners ?? throw new ArgumentException(nameof(owners));
            logger_ = logger ?? throw new ArgumentException(nameof(logger));
        }

       public async Task<ApiResult<Reward>> CreateRewardAsync(int ownerId,
            int projectId, Model.Options.CreateRewardOptions options)
        {
            if (ownerId <= 0 ||
              options == null ||
              projectId <= 0 ) {
                return new ApiResult<Reward>(
                    StatusCode.BadRequest, "Null options");
            }

            if (string.IsNullOrWhiteSpace(options.Title) ||
                 string.IsNullOrWhiteSpace(options.Description) ||
                     options.Value == 0) {
                return new ApiResult<Reward>(
                    StatusCode.BadRequest, "Null Title Or Description");
            }

            var owner = await owners_.SearchOwnerByIdAsync(ownerId);

            if (owner == null) {
                return new ApiResult<Reward>(
                     StatusCode.BadRequest, "Owner Was not Found");
            }

            var reward = new Reward()
            {
                Owner = owner.Data,
                Title = options.Title,
                Description = options.Description,
                Value = options.Value,
                ProjectId = projectId
            };

            var result = await owners_.AddRewardAsync(ownerId, reward);

            if (result == false) {
                return null;
            }

            context_.Add(reward);

            try {
              await  context_.SaveChangesAsync();
            } catch {
                logger_.LogError(StatusCode.InternalServerError,
                       $"Error Save Project: {reward.Title}");

                return new ApiResult<Reward>
                     (StatusCode.InternalServerError,
                       "Error  Creating Reward");
            }

             return ApiResult<Reward>.CreateSuccess(reward); 
        }

        public async Task<ApiResult<List<Reward>>> SearchRewardByProjectIdAsync(int projectId)
        {
            if (projectId <= 0) {
                return new ApiResult<List<Reward>>(
                       StatusCode.BadRequest, "Reward Invalid");
            }
            var rewardList = await context_
                .Set<Reward>()
                .Where(s => s.ProjectId == projectId)
                .ToListAsync();

            if (rewardList == null) {

                return new ApiResult<List<Reward>>(
                      StatusCode.BadRequest, "Reward Was Not Found");
            }

            return ApiResult<List<Reward>>.CreateSuccess(rewardList);
        }

        public async Task<ApiResult<Reward>> SearchRewardByIdAsync(int rewardId) {
            if (rewardId <= 0) {
                return new ApiResult<Reward>(
                       StatusCode.BadRequest, "Reward Invalid");
            }
            var reward = await context_
                .Set<Reward>()
                .Where(s => s.Id == rewardId)
                .SingleOrDefaultAsync();
            if (reward == null) {
                return new ApiResult<Reward>(
                      StatusCode.BadRequest, "Reward Was Not Found");
            }
            return ApiResult<Reward>.CreateSuccess(reward);
        }
    }
}
