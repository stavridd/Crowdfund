namespace Crowdfund.Core.Model {
    public class BuyerReward 
    {
        /// <summary>
        /// The id of the Backer
        /// </summary>
        public int BuyerId { get; set; }

        public Buyer Buyer { get; set; }

        /// <summary>
        /// The id of the reward
        /// </summary>
        public int RewardId { get; set; }

        public Reward Reward { get; set; }
    }
}