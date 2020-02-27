using Crowdfund.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdfund.Web.Models {
    public class CreateProjectViewModel {

        public Core.Model.Options.CreateProjectOptions CreateOptions { get; set; }

        

        public int OwnerId { get; set; }
        public string ErrorText { get; set; }

        public int projId { get; set; }

        public List<Reward> reward { get; set; }
    }
}
