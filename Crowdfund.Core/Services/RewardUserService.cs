using Crowdfund.Core.Data;
using Crowdfund.Core.Models;
using Crowdfund.Core.Services.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Core.Services
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

        public Result<bool> CreateOrUpdateRewardUser(CreateRewardUserOptions options)
        {
            if (options == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, "You have to login first");
            }
            var rewardUserTest = GetRewardUserById(options.UserId, options.RewardId);
            if (rewardUserTest.Success) //update
            {
                var rewardUser = rewardUserTest.Data;
                var project = projectService_.GetProjectByRewardId(options.RewardId).Data;

                if (rewardUser == null)
                {
                    return Result<bool>.CreateFailed(StatusCode.BadRequest, $"Backer with {options.UserId} was not found");
                }

                if (rewardUser.IsValidQuantity(options.Quantity))
                {
                    rewardUser.Quantity = options.Quantity + rewardUser.Quantity;
                }

                

                if (!projectService_.UpdateCurrentFund(project).Success)
                {
                    return Result<bool>.CreateFailed(StatusCode.InternalServerError, "Reward could not be bought");
                }

                return Result<bool>.CreateSuccessful(true);
            }
            else //create
            {
                var user = userService_.GetUserById(options.UserId).Data;
                var reward = rewardService_.GetRewardById(options.RewardId).Data;
                var project = projectService_.GetProjectByRewardId(options.RewardId).Data;
                if (user == null || reward == null || project == null)
                {
                    return Result<bool>.CreateFailed(StatusCode.BadRequest, "User, project or reward could not be found");
                }


                var rewardUser = new RewardUser()
                {
                    User = user,
                    Reward = reward,
                    Quantity = options.Quantity
                };

                if (!rewardUser.IsValidQuantity(options.Quantity))
                {
                    return Result<bool>.CreateFailed(StatusCode.BadRequest, "Please check the quantity is valid");
                }


                project.RewardUsers.Add(rewardUser);

                if (!projectService_.UpdateCurrentFund(project).Success)
                {
                    return Result<bool>.CreateFailed(StatusCode.InternalServerError, "Reward could not be bought");
                }

                return Result<bool>.CreateSuccessful(true);
               
            }
        }

        public IQueryable<Project> SearchProjectsFundedByUser(int userId)
        {
            
            var query = context_
                .Set<Project>()
                .AsQueryable()
                //.Include(a => a.RewardUsers)
                //.ThenInclude(b=>b.Reward)
                .Where(c => c.RewardUsers.Any(i => i.UserId == userId))
                .Include(c=>c.Media);

            return query;
        }

        public Result<RewardUser> GetRewardUserById(int userId, int rewardId)
        {
            
            var rewardUser = context_
                 .Set<RewardUser>()
                 .AsQueryable()
                 .Where(c => c.UserId == userId && c.RewardId == rewardId)
                 .SingleOrDefault();
            if (rewardUser == null)
            {
                return Result<RewardUser>.CreateFailed(StatusCode.NotFound, "Reward could not be found");
            }
            return Result<RewardUser>.CreateSuccessful(rewardUser);
        }

        public Result<bool> DeleteRewardUser(int userId, int rewardId) // refund
        {
            
            var rewardUser = GetRewardUserById(userId, rewardId).Data;

            if (rewardUser == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, $"Backer was not found");
            }

            var project = projectService_.GetProjectByRewardId(rewardId).Data;

            project.RewardUsers.Remove(rewardUser);
            context_.Remove(rewardUser);


            if (!projectService_.UpdateCurrentFund(project).Success)
            {
                return Result<bool>.CreateFailed(StatusCode.InternalServerError, "RewardUser could not be deleted");
            }

            return Result<bool>.CreateSuccessful(true);

        }
    }
}
