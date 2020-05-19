﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Services.Options
{
    public class CreateRewardOptions
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}