using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Models
{
    public class RewardUser
    {
        public int UserId { get; set; }
        public int RewardId { get; set; }
        public Reward Reward { get; set; }
        public User User { get; set; }
        public int Quantity { get; set; }

        //decimal moneyoffered??

        public bool IsValidQuantity(int quantity)
        {
            return quantity > 0;
        }
    }
}
