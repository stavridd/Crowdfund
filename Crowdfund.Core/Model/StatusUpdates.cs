namespace Crowdfund.Core.Model {
    public class StatusUpdates        
    {
        public int id { get; set; }

        public string statusUpdate { get; set; }

        Project project { get; set; }
    }
}