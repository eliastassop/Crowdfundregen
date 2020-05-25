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
    public class RewardService:IRewardService
    {
        private CrowdfundDB context_;
        private IProjectService projectService_;

        public RewardService(CrowdfundDB context, IProjectService projectService)
        {
            context_ = context;
            projectService_ = projectService;
        }

        public Result<Reward> CreateReward(CreateRewardOptions options)
        {
            if(options == null)
            {
                return Result<Reward>.CreateFailed(StatusCode.BadRequest, "Null options");
            }
            var project = projectService_.GetProjectById(options.ProjectId);

            if (project == null)
            {
                return Result<Reward>.CreateFailed(StatusCode.BadRequest, $"Project with {options.ProjectId} was not found");
            }

            var reward = new Reward()
            {
                Title = options.Title,
                Description = options.Description,
                Price = options.Price,
            };
            if(!reward.IsValidDescription(options.Description)
                || !reward.IsValidTitle(options.Title)
                || !reward.IsValidPrice(options.Price))
            {
                return Result<Reward>.CreateFailed(StatusCode.BadRequest, "Please check the validations");
            }
            project.AvailableRewards.Add(reward);

            if (context_.SaveChanges() <= 0)
            {
                return Result<Reward>.CreateFailed(
                    StatusCode.InternalServerError,
                    "Reward could not be created");
            }

            return Result<Reward>.CreateSuccessful(reward);
        }

        public IQueryable<Reward> SearchRewards(SearchRewardOptions options) // SearchRewardByProjectId + TA ALLA
        {
            var query = context_
                 .Set<Reward>()
                 .AsQueryable();

            if (options == null)
            {
                return null;
            }

            if (options.ProjectId != null)
            {

                var project = projectService_.GetProjectById(options.ProjectId);

                query = query.Where(c => project.AvailableRewards.Contains(c)); // ferno ola ta available rewards
            }

            
            if (options.RewardId != null)
            {
                query = query.Where(r => r.RewardId == options.RewardId);  // filtraro analoga me to RewardId
            }

            query = query.Take(500);
            return query;
        }

        public IQueryable<Reward> SearchRewardsByProjectId(int? projectId)
        {
            if(projectId == null)
            {
                return null;
            }

            var rewardQuery = SearchRewards(new SearchRewardOptions()
            {
                ProjectId = projectId,
            });

            return rewardQuery;
        }


        public Reward GetRewardById(int? rewardId)
        {
            if (rewardId == null)
            {
                return null;
            }

            var reward = SearchRewards(new SearchRewardOptions()
            {
                  RewardId = rewardId,
            }).SingleOrDefault();
            return reward;
        }

        public Result<bool> DeleteReward(int? rewardId)
        {
            if (rewardId == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, "Null options for id");
            }
            var reward = GetRewardById(rewardId);
            if (reward == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, $"Reward with {rewardId} was not found");
            }

            context_.Remove(reward);
            if (context_.SaveChanges() == 0)
            {
                return Result<bool>.CreateFailed(StatusCode.InternalServerError, "Something went wrong");
            }

            return Result<bool>.CreateSuccessful(true);
        }

        public Result<bool> UpdateReward(UpdateMediaOption options)
        {
            if (options == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, "Null options");
            }

            var reward = GetRewardById(options.RewardId);
            if (reward == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, $"Reward with {options.RewardId} was not found");
            }

            if (reward.IsValidTitle(options.Title))
            {
                reward.Title = options.Title;
            }

            if(reward.IsValidDescription(options.Description))
            {
                reward.Description = options.Description;
            }

            if(reward.IsValidPrice(options.Price))
            {
                reward.Description = options.Description;
            }

            if (context_.SaveChanges() == 0)
            {
                return Result<bool>.CreateFailed(StatusCode.InternalServerError, "Something went wrong");
            }

            return Result<bool>.CreateSuccessful(true);
        }

        

    }
}
