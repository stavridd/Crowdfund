using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Crowdfund.Core.Model {
    public class ProjectBuyer {

        /// <summary>
        /// The id of the backer of 
        /// the project
        /// </summary>
        public int BuyerId { get; set; }

        /// <summary>
        /// The Backer (Navigation property)
        /// </summary>
        public Buyer Buyer { get; set; }


        /// <summary>
        /// The id of the project 
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// The project (Navigation property)
        /// </summary>
        [JsonIgnore]
        public Project Project { get; set; }
    }
}
