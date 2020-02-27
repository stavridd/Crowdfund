namespace Crowdfund.Core.Model {
    public class BuyerReward 
    {
        /// <summary>
        /// The id of the Backer
        /// </summary>
        public int BuyerId { get; set; }


        /// <summary>
        /// Navigation property for Entity Framework
        /// </summary>
        public Buyer Buyer { get; set; }

        /// <summary>
        /// The id of the reward
        /// </summary>
        public int RewardId { get; set; }


        /// <summary>
        /// Navigation property for Entity Frameworl
        /// </summary>
        public Reward Reward { get; set; }
    }
}