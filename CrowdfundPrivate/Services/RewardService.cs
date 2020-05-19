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

        public bool CreateReward(CreateRewardOptions options)
        {
            if(options == null)
            {
                return false;
            }
            var project = projectService_.GetProjectById(options.ProjectId);

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
                return false;
            }
            project.AvailableRewards.Add(reward);

            if(context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;
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

        public bool DeleteReward(int? rewardId)
        {
            if(rewardId == null)
            {
                return false;
            }
            var reward = GetRewardById(rewardId);
            
            context_.Remove(reward);
            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public bool UpdateReward(UpdateRewardOptions options)
        {
            if(options == null)
            {
                return false;
            }

            var reward = GetRewardById(options.RewardId);

            if (reward == null)
            {
                return false;
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

            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public Project GetProjectByRewardId(int? rewardId)
        {
            if (rewardId == null)
            {
                return null;
            }
            var query = context_
                .Set<Project>()
                .AsQueryable();
            query = query
                    .Include(a => a.AvailableRewards)
                    .Where(c => c.AvailableRewards.Contains(GetRewardById(rewardId)));
            return query.SingleOrDefault();  
        }

    }
}
