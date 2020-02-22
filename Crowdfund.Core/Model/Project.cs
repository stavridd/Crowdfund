using System.Collections.Generic;

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
        public ICollection<Multimedia> Multis { get; set; }

        /// <summary>
        /// A list of status updates of the project 
        /// </summary>
        public ICollection<StatusUpdates> Updates { get; set; }

        /// <summary>
        /// The category of the project 
        /// </summary>
        public ProjectCategory projectcategory { get; set; }

        public ProjectStatus Status { get; set; } 

        /// <summary>
        /// The views of the project
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// A list of the contributors 
        /// </summary>
        public ICollection<ProjectBuyer> Buyers { get; set; }

        /// <summary>
        /// The project creator
        /// </summary>
        public Owner Owner { get; set; }

        public int OwnerId { get; set; }

        public decimal Goal { get; set; }

        public int NumberOfContributions { get; set; }

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