using System.Text.Json.Serialization;

namespace Crowdfund.Core.Model {
    public class Multimedia 
    {
        public int Id { get; set; }
        [JsonIgnore]
        public Project project { get; set; }
        public int projectId { get; set; }

        public string url { get; set; }

        public MultimediaCategory multimediaCategory { get; set; }
    }
}