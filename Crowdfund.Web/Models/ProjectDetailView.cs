using Crowdfund.Core.Model;
using Crowdfund.Core.Services;
using System.Collections.Generic;

namespace Crowdfund.Web.Models {
    public class ProjectDetailView {

        public string Title { get; set; }


        public string Description { get; set; }

        public ProjectCategory projectcategory { get; set; }

        public decimal Goal { get; set; }

        public ICollection<Multimedia> Multis { get; set; }

        public ICollection<StatusUpdates> Updates { get; set; }

        public decimal Contributions { get; set; }

        public decimal progress { get; set; }

        public ProjectCategory ProjectCategory { get; set; }

        public ApiResult<List<Reward>> Rewards { get; set; }
    }
}
