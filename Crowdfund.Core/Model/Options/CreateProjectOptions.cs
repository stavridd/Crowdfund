namespace Crowdfund.Core.Model.Options {
    public class CreateProjectOptions 
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public ProjectCategory projectcategory { get; set; }
    }
}
