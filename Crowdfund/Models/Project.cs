using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Models
{
    public class Project
    {
        int ProjectId { get; set; }
        string ProjectTitle { get; set; }
        decimal CurrentFund { get; set; }
        decimal TotalFund { get; set; }
        string VideoLink { get; set; }
        string PhotoLink { get; set; }       
        DateTime ProjectCreated { get; set; }
        DateTime ProjectDeadline { get; set; }
        List<Reward> AvailableRewards { get; set; }
        List<RewardBacker> RewardBackers { get; set; }
        public Project()
        {
            AvailableRewards = new List<Reward>();
            RewardBackers = new List<RewardBacker>();
        }
    }
}
