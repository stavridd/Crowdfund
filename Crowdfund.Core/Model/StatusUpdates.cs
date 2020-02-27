using System;
using System.Text.Json.Serialization;

namespace Crowdfund.Core.Model {
    public class StatusUpdates        
    {
        public int id { get; set; }

        public int projectId { get; set; }

        public string statusUpdate { get; set; }

        public DateTimeOffset DatePost { get; set; }

        [JsonIgnore]
        public Project project { get; set; }

        public StatusUpdates() {
            DatePost = DateTimeOffset.Now;
        }
    }
}