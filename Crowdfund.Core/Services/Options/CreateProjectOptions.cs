using Crowdfund.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Services.Options
{
    public class CreateProjectOptions
    {
        public int CreatorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalFund { get; set; }
        public string Category { get; set; }
        public DateTime Deadline { get; set; }
        //public List<Media> Media { get; set; }
    }
}
