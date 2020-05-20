using Crowdfund.Models;
using Crowdfund.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Services
{
    public interface IRewardService
    {
        bool CreateReward(CreateRewardOptions options);
        IQueryable<Reward> SearchRewards(SearchRewardOptions options);

        IQueryable<Reward> SearchRewardsByProjectId(int? projectId);
        Reward GetRewardById(int? rewardId);

        bool UpdateReward(UpdateMediaOption options);

        bool DeleteReward(int? rewardId);        
    }
}
