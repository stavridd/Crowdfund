using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Crowdfund.Core.Model;

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

       public Reward CreateReward(int ownerId,
            Model.Options.CreateRewardOptions options)
        {
            if (ownerId <= 0 ||
              options == null) {
                return null;
            }

            if (string.IsNullOrWhiteSpace(options.Title) ||
                 string.IsNullOrWhiteSpace(options.Description) ||
                     options.Value == 0) {
                return null;
            }

            var owner = owners_.SearchOwnerById(ownerId);

            if (owner == null) {
                return null;
            }

            var reward = new Reward()
            {
                Owner = owner,
                Title = options.Title,
                Description = options.Description,
                Value = options.Value
            };

            var result = owners_.AddReward(ownerId, reward);

            if (result == false) {
                return null;
            }

            context_.Add(reward);

            try {
                context_.SaveChanges();
            } catch (Exception ex) {
                return null;
            }

            return reward;
        }

        public Reward SearchRewardById(int rewardId)
        {
            if (rewardId <= 0) {
                return null;
            }
            return context_
                .Set<Reward>()
                .Where(s => s.Id == rewardId)
                .SingleOrDefault();
        }
    }
}
