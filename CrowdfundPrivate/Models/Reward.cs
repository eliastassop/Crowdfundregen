using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Models
{
    public class Reward
    {
        public int RewardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        //datetime estimatedprojectdelivery???
    }
}
