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
        Result<Reward> CreateReward(CreateRewardOptions options);
        Result<bool> UpdateReward(UpdateMediaOption options);
        Result<bool> DeleteReward(int? rewardId);
        IQueryable<Reward> SearchRewards(SearchRewardOptions options);
        IQueryable<Reward> SearchRewardsByProjectId(int? projectId);
        Reward GetRewardById(int? rewardId);

        

                
    }
}
