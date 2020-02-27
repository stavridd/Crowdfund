using System;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Model;
using System.Collections.Generic;

namespace Crowdfund.Web.Models
{
    public class ProjectsViewModel
    {

        public List<Project> Projects { get; set; }

        public List<Project> OwnerProjects { get; set; }

        public List<Project> BuyerProjects { get; set; }

    }
}
