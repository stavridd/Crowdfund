using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Model.Options {
    public class CreateRewardOptions 
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Value { get; set; }
    }
}
