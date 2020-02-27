using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Crowdfund.Web.Models
{
    public class CreateBuyerViewModel
    {

        public Core.Model.Options.CreateBuyerOptions CreateOptions { get; set; }
        
        public string ErrorText { get; set; }
    }
}
