using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Model;

namespace Crowdfund.Web.Models {
    public class ProjectsViewModel {

        public List<Project> Projects { get; set; }
        public List<Project> OwnerProjects { get; set; }

        public List<Project> BuyerProjects { get; set; }

    }
}
