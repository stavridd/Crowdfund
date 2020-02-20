using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crowdfund.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace Crowdfund.Core.Services {
    public class RewardService : IRewardService 
    {
        private readonly Data.CrowdfundDbContext context_;
        private readonly IOwnerService owners_;
        //private readonly IBuyerService buyers_;

        public RewardService(Data.CrowdfundDbContext context,
            IOwnerService owners)
        {
            context_ = context ??
                throw new ArgumentException(nameof(context));
            owners_ = owners;
            //buyers_ = buyer;
        }

       public async Task<ApiResult<Reward>> CreateRewardAsync(int ownerId,
            Model.Options.CreateRewardOptions options)
        {
            if (ownerId <= 0 ||
              options == null) {
                return new ApiResult<Reward>(
                    StatusCode.BadRequest, "Null options");
            }

            if (string.IsNullOrWhiteSpace(options.Title) ||
                 string.IsNullOrWhiteSpace(options.Description) ||
                     options.Value == 0) {
                return new ApiResult<Reward>(
                    StatusCode.BadRequest, "Null Title Or Description");
            }

            //RE FUGE  RE MALAKA apo dw RE BRO
            var owner = await owners_.SearchOwnerByIdAsync(ownerId);

            if (owner == null) {
                return new ApiResult<Reward>(
                     StatusCode.BadRequest, "Owner Was not Found");
            }

            var reward = new Reward()
            {
                Owner = owner,
                Title = options.Title,
                Description = options.Description,
                Value = options.Value   
            };

            var result = await owners_.AddRewardAsync(ownerId, reward);

            if (result == false) {
                return null;
            }

            context_.Add(reward);

            try {
              await  context_.SaveChangesAsync();
            } catch (Exception ex) {
                return new ApiResult<Reward>(
                      StatusCode.InternalServerError, "Reward Was Not Added");
            }

             return ApiResult<Reward>.CreateSuccess(reward); 
        }

        public async Task<ApiResult<Reward>> SearchRewardByIdAsync(int rewardId)
        {
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
