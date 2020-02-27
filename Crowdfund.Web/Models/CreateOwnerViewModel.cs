using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Crowdfund.Web.Models
{
    public class CreateOwnerViewModel
    {

        public Core.Model.Options.CreateOwnerOptions CreateOptions { get; set; }
        
        public string ErrorText { get; set; }
    }
}
