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
        bool CreateRewardUser(CreateRewardUserOptions options);
        bool UpdateRewardUser(UpdateRewardUserOptions options);
        IQueryable<RewardUser> SearchRewardUsers(SearchRewardUserOptions options);
        RewardUser GetRewardUserById(int? rewardUserId);
        bool DeleteRewardUser(int? rewardUserId);
    }
}
