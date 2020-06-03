using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal CurrentFund { get; set; }
        public decimal TotalFund { get; set; }        
        public ProjectCategory Category { get; set; }
        public DateTime Created { get; set; }
        public DateTime Deadline { get; set; }
        public List<Reward> AvailableRewards { get; set; }
        public List<RewardUser> RewardUsers { get; set; }
        public List<Media> Media { get; set; }
        public List<StatusUpdate> StatusUpdates { get; set; }

        public Project()
        {
            AvailableRewards = new List<Reward>();
            RewardUsers = new List<RewardUser>();
            Created = DateTime.Now;
            Media = new List<Media>();
            StatusUpdates = new List<StatusUpdate>();
        }

        public bool IsValidTitle(string title)
        {
            return !string.IsNullOrWhiteSpace(title) && title.Length <= 50;
        }

        public bool IsValidDescription(string description)
        {
            return !string.IsNullOrWhiteSpace(description) && description.Length <= 10000;
        }

        public bool IsValidTotalFund(decimal totalFund)
        {
            return totalFund > 0;
        }
        public bool IsValidCategory(ProjectCategory category)
        {
            return Enum.IsDefined(typeof(ProjectCategory), category);             
        }
        public bool IsValidDeadline(DateTime deadline)
        {
            return deadline > DateTime.Now;
        }
    }
}
