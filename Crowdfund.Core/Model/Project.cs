using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Crowdfund.Core.Model {
    public class Project {

        /// <summary>
        /// The id of the project
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The title of the project
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description of the project
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A list of photos or videos url of the project 
        /// </summary>
        [JsonIgnore]
        public ICollection<Multimedia> Multis { get; set; }

        /// <summary>
        /// A list of status updates of the project 
        /// </summary>
        [JsonIgnore]
        public ICollection<StatusUpdates> Updates { get; set; }

        /// <summary>
        /// The category of the project 
        /// </summary>
        public ProjectCategory projectcategory { get; set; }

        /// <summary>
        /// The status of the project
        /// </summary>
        public ProjectStatus Status { get; set; } 

        /// <summary>
        /// The views of the project
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// A list of the contributors 
        /// </summary>
        [JsonIgnore]
        public ICollection<ProjectBuyer> Buyers { get; set; }

        /// <summary>
        /// The project creator
        /// </summary>
        [JsonIgnore]
        public Owner Owner { get; set; }

        /// <summary>
        /// The id of the owner
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// The financial goal of the project
        /// </summary>
        public decimal Goal { get; set; }

        /// <summary>
        /// The number of the backers who 
        /// buy a reward
        /// </summary>
        public int NumberOfContributions { get; set; }

        /// <summary>
        /// The amount of contributions
        /// </summary>
        public decimal Contributions { get; set; }

        public Project()
        {
            Buyers = new List<ProjectBuyer>();
            Multis = new List<Multimedia>();
            Updates = new List<StatusUpdates>();
            Status = ProjectStatus.Active;
            Contributions = 0;
        }
    }
}