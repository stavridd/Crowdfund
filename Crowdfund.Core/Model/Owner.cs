using System.Threading.Tasks;
using System.Collections.Generic;


namespace Crowdfund.Core.Model {
    public class Owner {

        /// <summary>
        /// The id of the Project Creator
        /// </summary>
        public int Id { get; set; }

        /// <summary>/
        /// The FirstName of the Project Creator
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The LastName of the Project Creator
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The Emai of the Project Creator
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The age of the Project Creator
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// The Photo of the Project Creator
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// A list with all the projects of the Project Creator
        /// </summary>
        public  ICollection<Project> Projects { get; set; }

        /// <summary>
        /// A list with all the project Rewards
        /// tha the Project Creator gives
        /// </summary>
        public ICollection<Reward> Rewards { get; set; }

        public Owner()
        {
            Projects = new List<Project>();
            Rewards = new List<Reward>();
        }
    }
}
