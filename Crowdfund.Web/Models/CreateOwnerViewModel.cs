using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdfund.Web.Models {
    public class CreateOwnerViewModel {


        public Core.Model.Options.CreateOwnerOptions CreateOptions { get; set; }
        public string ErrorText { get; set; }


    }
}
