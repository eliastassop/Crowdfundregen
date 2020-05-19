using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Services.Options
{
    public class UpdateRewardUserOptions
    {
        public int UserId { get; set; }
        public int RewardId { get; set; }
        public int Quantity { get; set; }
    }
}
