﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Services.Options
{
    public class CreateRewardUserOptions
    {
        public int UserId { get; set; }        
        public int RewardId { get; set; }
        public int Quantity { get; set; }
    }
}
