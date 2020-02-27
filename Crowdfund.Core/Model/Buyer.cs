using System.Collections.Generic;

namespace Crowdfund.Core.Model {
    public class Buyer {

        /// <summary>
        /// The id of the Backer
        /// </summary>
        public int Id { get; set; }

        /// <summary>/
        /// The FirstName of the Backer
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The LastName of the Backer
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The Email of the Backer
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The age of the Backer
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// The Photo of the Backer
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// A list with all the projects that the Backer
        /// contribute to
        /// </summary>
        public ICollection<ProjectBuyer> Projects { get; set; }

        /// <summary>
        /// A list with all the project Rewards
        /// tha the Backer took
        /// </summary>
        public ICollection<BuyerReward> Rewards { get; set; }

        public Buyer()
        {
            Projects = new List<ProjectBuyer>();
            Rewards = new List<BuyerReward>();
        }
    }
}
