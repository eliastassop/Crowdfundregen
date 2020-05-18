using Crowdfund.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Services.Options
{
    public class UpdateProjectOptions
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal TotalFund { get; set; }
        public ProjectCategory Category { get; set; }
        public DateTime Deadline { get; set; }
        //public List<Media> Media { get; set; }
    }
}
