using Crowdfund.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Services.Options
{
    public class SearchProjectOptions
    {
        public int? CreatorId { get; set; }
        public int? BackerId { get; set; }
        public int? ProjectId { get; set; }
        public string SearchText { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal? CurrentFundFrom { get; set; }
        public decimal? TotalFundFrom { get; set; }
        public ProjectCategory? Category { get; set; }
        public DateTime? CreatedFrom { get; set; }
        public DateTime? DeadlineFrom { get; set; }
        public int? RewardId { get; set; }
        public int? StatusUpdateId { get; set; }
    }
}
