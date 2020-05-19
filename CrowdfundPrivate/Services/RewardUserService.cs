using Crowdfund.Data;
using Crowdfund.Models;
using Crowdfund.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Services
{
    public class RewardUserService:IRewardUserService
    {
        private CrowdfundDB context_;
        private IUserService userService_;
        private IProjectService projectService_;
        private IRewardService rewardService_;
        public RewardUserService(CrowdfundDB context, IUserService userService,
            IProjectService projectService,IRewardService rewardService)
        {
            context_ = context;
            userService_ = userService;
            projectService_ = projectService;
            rewardService_ = rewardService;
        }

        public bool CreateRewardUser(CreateRewardUserOptions options)
        {
            if (options == null)
            {
                return false; //Request anti gia false
            }
            var user = userService_.GetUserById(options.UserId);
            var reward = rewardService_.GetRewardById(options.RewardId);
            var project = projectService_.GetProjectByRewardId(options.RewardId);
            var rewardUser = new RewardUser()
            {
                User = user,
                Reward=reward,
                Quantity=options.Quantity
            };

            //validation prin mpei sti vasi
            project.RewardUsers.Add(rewardUser);            
            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;

        }

        public bool SearchProjectsFundedByUser(int? userId)
        {
            if (userId == null)
            {
                return false; //Request anti gia false
            }
            var rewardUsers = context_
                .Set<RewardUser>()
                .AsQueryable()
                .Where(c => c.UserId == userId)                
                .ToList();
            
            var user = userService_.GetUserById(options.UserId);
            var reward = rewardService_.GetRewardById(options.RewardId);
            var project = projectService_.GetProjectByRewardId(options.RewardId);
            var rewardUser = new RewardUser()
            {
                User = user,
                Reward = reward,
                Quantity = options.Quantity
            };

            //validation prin mpei sti vasi
            project.RewardUsers.Add(rewardUser);
            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;

        }
    }
}
