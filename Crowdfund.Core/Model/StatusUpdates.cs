using System;
using System.Text.Json.Serialization;

namespace Crowdfund.Core.Model {
    public class StatusUpdates        
    {

        /// <summary>
        /// The id of the status update
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The id if the project that
        /// this status update refers to
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// The status update
        /// </summary>
        public string StatusUpdate { get; set; }

        /// <summary>
        /// The date is created
        /// </summary>
        public DateTimeOffset DatePost { get; set; }

        /// <summary>
        /// The project (navigation property)
        /// </summary>
        [JsonIgnore]
        public Project Project { get; set; }

        public StatusUpdates() {
            DatePost = DateTimeOffset.Now;
        }
    }
}