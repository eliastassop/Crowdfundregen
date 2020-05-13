using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Models
{
    public class Reward
    {
        int RewardId { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }

        //datetime estimatedprojectdelivery???
    }
}
