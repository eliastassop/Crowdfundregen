using Crowdfund.Data;
using Crowdfund.Models;
using Crowdfund.Services.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Services
{
    public class RewardUserService : IRewardUserService
    {
        private CrowdfundDB context_;
        private IUserService userService_;
        private IProjectService projectService_;
        private IRewardService rewardService_;
        public RewardUserService(CrowdfundDB context, IUserService userService,
            IProjectService projectService, IRewardService rewardService)
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
            if (user == null || reward == null || project == null)
            {
                return false;
            }

            var rewardUser = new RewardUser()
            {
                User = user,
                Reward = reward,
                Quantity = options.Quantity
            };

            if (!rewardUser.IsValidQuantity(options.Quantity))
            {
                return false;
            }


            project.RewardUsers.Add(rewardUser);

            projectService_.CalculateCurrentFund(project);
            
            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public IQueryable<Project> SearchProjectsFundedByUser(int? userId)
        {
            if (userId == null)
            {
                return null; //Request anti gia false
            }

            var query = context_
                .Set<Project>()
                .AsQueryable()
                //.Include(a => a.RewardUsers)
                //.ThenInclude(b=>b.Reward)
                .Where(c => c.RewardUsers.Any(i => i.UserId == userId));

            return query;
        }

        public RewardUser GetRewardUserById(int? userId, int? rewardId)
        {
            if (userId == null || rewardId == null)
            {
                return null;
            }
            var rewardUser = context_
                 .Set<RewardUser>()
                 .AsQueryable()
                 .Where(c => c.UserId == userId && c.RewardId == rewardId)
                 .SingleOrDefault();
            return rewardUser;
        }

        public bool UpdateRewardUser(UpdateRewardUserOptions options)
        {
            if (options == null)
            {
                return false;
            }
            var rewardUser = GetRewardUserById(options.UserId, options.RewardId);
            var project = projectService_.GetProjectByRewardId(options.RewardId);
            
            if (rewardUser == null)
            {
                return false;
            }
            if (rewardUser.IsValidQuantity(options.Quantity))
            {
                rewardUser.Quantity = options.Quantity + rewardUser.Quantity;
                

            }
            projectService_.CalculateCurrentFund(project);
            
            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteRewardUser(int? userId, int? rewardId)
        {
            if (userId == null || rewardId == null)
            {
                return false;
            }
            var rewardUser = GetRewardUserById(userId, rewardId);
            context_.Remove(rewardUser);
            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
    }
}
