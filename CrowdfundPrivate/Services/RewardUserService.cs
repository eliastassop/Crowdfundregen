﻿using Crowdfund.Data;
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

        public Result<RewardUser> CreateRewardUser(CreateRewardUserOptions options)
        {
            if (options == null)
            {
                return Result<RewardUser>.CreateFailed(StatusCode.BadRequest, "Null options");
            }
            
            var user = userService_.GetUserById(options.UserId);
            var reward = rewardService_.GetRewardById(options.RewardId);
            var project = projectService_.GetProjectByRewardId(options.RewardId);
            if (user == null || reward == null || project == null)
            {
                return Result<RewardUser>.CreateFailed(StatusCode.InternalServerError, "Something went wrong");
            }

            var rewardUser = new RewardUser()
            {
                User = user,
                Reward = reward,
                Quantity = options.Quantity
            };

            if (!rewardUser.IsValidQuantity(options.Quantity))
            {
                return Result<RewardUser>.CreateFailed(StatusCode.BadRequest, "Please check the options for quantity");
            }


            project.RewardUsers.Add(rewardUser);

            projectService_.CalculateCurrentFund(project);

            if (context_.SaveChanges() <= 0)
            {
                return Result<RewardUser>.CreateFailed(
                    StatusCode.InternalServerError,
                    "Backer could not be created");
            }

            return Result<RewardUser>.CreateSuccessful(rewardUser);
        }

        public IQueryable<Project> SearchProjectsFundedByUser(int userId)
        {
            
            var query = context_
                .Set<Project>()
                .AsQueryable()
                //.Include(a => a.RewardUsers)
                //.ThenInclude(b=>b.Reward)
                .Where(c => c.RewardUsers.Any(i => i.UserId == userId));

            return query;
        }

        public RewardUser GetRewardUserById(int userId, int rewardId)
        {
            
            var rewardUser = context_
                 .Set<RewardUser>()
                 .AsQueryable()
                 .Where(c => c.UserId == userId && c.RewardId == rewardId)
                 .SingleOrDefault();
            return rewardUser;
        }

        public Result<bool> UpdateRewardUser(int rewardId, int userId, int quantity)
        {
            
            var rewardUser = GetRewardUserById(userId, rewardId);
            var project = projectService_.GetProjectByRewardId(rewardId);

            if (rewardUser == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, $"Backer with {userId} was not found");
            }
            
            if (rewardUser.IsValidQuantity(quantity))
            {
                rewardUser.Quantity = quantity + rewardUser.Quantity;
            }

            projectService_.CalculateCurrentFund(project);

            if (context_.SaveChanges() == 0)
            {
                return Result<bool>.CreateFailed(StatusCode.InternalServerError, "Something went wrong");
            }

            return Result<bool>.CreateSuccessful(true);
        }

        public Result<bool> DeleteRewardUser(int userId, int rewardId) // refund
        {
            
            var rewardUser = GetRewardUserById(userId, rewardId);

            if (rewardUser == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, $"Backer was not found");
            }

            var project = projectService_.GetProjectByRewardId(rewardId);

            project.RewardUsers.Remove(rewardUser);
            context_.Remove(rewardUser);
            

            projectService_.CalculateCurrentFund(project);

            if (context_.SaveChanges() == 0)
            {
                return Result<bool>.CreateFailed(StatusCode.InternalServerError, "Something went wrong");
            }

            return Result<bool>.CreateSuccessful(true);
        }
    }
}
