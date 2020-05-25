using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Services.Options
{
    public class SearchRewardOptions
    {
        public int? ProjectId { get; set; }

        public  int? RewardId { get; set; }

        //public int BackerId { get; set; }

        public decimal PriceFrom { get; set; }
    }
}
