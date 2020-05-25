using Crowdfund.Models;
using Crowdfund.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Services
{
    public interface IRewardUserService
    {
        Result<RewardUser> CreateRewardUser(CreateRewardUserOptions options);
        Result<bool> UpdateRewardUser(int rewardId, int userId, int quantity);
        Result<bool> DeleteRewardUser(int userId, int rewardId);
        //bool UpdateRewardUser(UpdateRewardUserOptions options);
        IQueryable<Project> SearchProjectsFundedByUser(int userId);
        RewardUser GetRewardUserById(int userId, int rewardId);
        
    }
}
