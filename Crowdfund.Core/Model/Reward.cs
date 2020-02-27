using System.Collections.Generic;

namespace Crowdfund.Core.Model {
    public class Reward {

        /// <summary>
        /// The id of the reward
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the reward
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description of the reward
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The value of the reward
        /// </summary>
        public decimal Value { get; set; }

        /// <summary>
        /// The project creator (navigation property)
        /// </summary>
        public Owner Owner { get; set; }

        /// <summary>
        /// A list with all the project Backer
        /// </summary>
        public ICollection<BuyerReward> Buyers { get; set; }

        /// <summary>
        /// The id of the project that this
        /// reward refers to
        /// </summary>
        public int ProjectId { get; set; }

        public Reward()
        {
            Buyers = new List<BuyerReward>();
        }
    }
}