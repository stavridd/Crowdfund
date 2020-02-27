using System;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Model;
using System.Collections.Generic;


namespace Crowdfund.Web.Models
{
    public class CreateProjectViewModel
    {

        public Core.Model.Options.CreateProjectOptions CreateOptions { get; set; }
        
        public int OwnerId { get; set; }
       
        public string ErrorText { get; set; }
        
        public int ProjId { get; set; }
        
        public List<Reward> Reward { get; set; }
       
        public decimal Progress { get; set; }
    }


}
