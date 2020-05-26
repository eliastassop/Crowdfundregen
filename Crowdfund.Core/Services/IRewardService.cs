using Crowdfund.Core.Models;
using Crowdfund.Core.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Core.Services
{
    public interface IRewardService
    {
        Result<Reward> CreateReward(CreateRewardOptions options);
        Result<bool> UpdateReward(int rewardId,UpdateRewardOptions options);
        Result<bool> DeleteReward(int rewardId);
        IQueryable<Reward> SearchRewards(SearchRewardOptions options);
        IQueryable<Reward> SearchRewardsByProjectId(int projectId);
        Result<Reward> GetRewardById(int rewardId);

        

                
    }
}
