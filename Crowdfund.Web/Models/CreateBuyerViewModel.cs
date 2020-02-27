using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdfund.Web.Models {
    public class CreateBuyerViewModel {

        public Core.Model.Options.CreateBuyerOptions CreateOptions { get; set; }
        public string ErrorText { get; set; }
    }
}
