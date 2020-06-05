using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Models
{
    public class Reward
    {
        public int RewardId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ProjectId { get; set; }

        //datetime estimatedprojectdelivery???

        public bool IsValidTitle(string title)
        {
            return !string.IsNullOrWhiteSpace(title) && title.Length <= 50;
        }

        public bool IsValidDescription(string description)
        {
            return !string.IsNullOrWhiteSpace(description) && description.Length <= 500;
        }

        public bool IsValidPrice(decimal price)
        {
            return price >= 0;
        }
    }
}
