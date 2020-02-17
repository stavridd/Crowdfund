using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Model {
    public class ProjectBuyer {
        /// <summary>
        /// The id of the backer of 
        /// the project
        /// </summary>
        public int BuyerId { get; set; }

        /// <summary>
        /// The Backer
        /// </summary>
        public Buyer Buyer { get; set; }


        /// <summary>
        /// The id of the project 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// The project
        /// </summary>
        public Project Project { get; set; }
    }
}
