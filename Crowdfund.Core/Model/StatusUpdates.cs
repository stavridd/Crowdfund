using System;

namespace Crowdfund.Core.Model {
    public class StatusUpdates        
    {
        public int id { get; set; }

        public int projectId { get; set; }

        public string statusUpdate { get; set; }

        public DateTimeOffset DatePost { get; set; }

        public Project project { get; set; }
    }
}