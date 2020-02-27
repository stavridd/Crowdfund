using System.Text.Json.Serialization;

namespace Crowdfund.Core.Model {
    public class Multimedia 
    {

        /// <summary>
        /// The id of the multimedia
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Navigation property for Entity Framework
        /// </summary>
        [JsonIgnore]
        public Project Project { get; set; }

        /// <summary>
        /// The id of the project 
        /// that the media belongs to
        /// </summary>
        public int ProjectId { get; set; }


        /// <summary>
        /// The url of the project
        /// </summary>
        public string Url { get; set; }

        public MultimediaCategory MultimediaCategory { get; set; }
    }
}