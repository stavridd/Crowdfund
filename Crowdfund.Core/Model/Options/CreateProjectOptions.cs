

using System.Collections.Generic;

namespace Crowdfund.Core.Model.Options {
    public class CreateProjectOptions 
    {
      
        public string Title { get; set; }

        
        public string Description { get; set; }

        public ProjectCategory projectcategory { get; set; }
        
        public decimal Goal { get; set; }

        public string Multis { get; set; }

        public List<Reward>? reward { get; set; }
    }
}
