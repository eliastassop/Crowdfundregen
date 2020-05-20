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
    public class ProjectService:IProjectService
    {
        private CrowdfundDB context_;
        private IUserService userService_;
        
        public ProjectService(CrowdfundDB context, IUserService userService)
        {
            context_ = context;
            userService_ = userService;            
        }
        public bool CreateProject(CreateProjectOptions options)
        {
            if (options == null)
            {
                return false; //Request anti gia false
            }
            var user = userService_.GetUserById(options.CreatorId);
            var project = new Project()
            {
                Title = options.Title,
                Description = options.Description,
                TotalFund = options.TotalFund,
                Category = options.Category,
                Deadline = options.Deadline
            };
            if (!project.IsValidCategory(options.Category) 
                || !project.IsValidDeadline(options.Deadline) 
                || !project.IsValidDescription(options.Description) 
                || !project.IsValidTitle(options.Title) 
                || !project.IsValidTotalFund(options.TotalFund))
            {
                return false;
            }
            //validation prin mpei sti vasi
            user.Projects.Add(project);
            //context_.Add(user);
            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;

        }        

        public IQueryable<Project> SearchProjects(SearchProjectOptions options)
        {
            if (options == null)
            {
                return null;
            }

            var query = context_
                 .Set<Project>()
                 .AsQueryable();

            //Probably not used -->
            if (!string.IsNullOrWhiteSpace(options.Title))
            {
                query = query.Where(c => c.Title == options.Title);
            }

            if (!string.IsNullOrWhiteSpace(options.Description))
            {
                query = query.Where(c => c.Description == options.Description);
            }
            //<--
            if (!string.IsNullOrWhiteSpace(options.SearchText))
            {
                query = query.Where(c => c.Title.Contains(options.SearchText)||c.Description.Contains(options.SearchText));
            }

            if (options.ProjectId != null)
            {
                query = query.Where(c => c.ProjectId == options.ProjectId.Value)
                    .Include(a=>a.AvailableRewards); //theloume to value?
            }

            if (options.DeadlineFrom != null)
            {
                query = query.Where(c => c.Deadline >= options.DeadlineFrom);
            }

            if (options.CreatedFrom != null)
            {
                query = query.Where(c => c.Created >= options.CreatedFrom);
            }

            if (options.Category != null)
            {
                query = query.Where(c => c.Category == options.Category.Value); 
            }

            if (options.TotalFundFrom != null)
            {
                query = query.Where(c => c.TotalFund >= options.TotalFundFrom.Value);
            }

            if (options.CurrentFundFrom != null)
            {
                query = query.Where(c => c.CurrentFund == options.CurrentFundFrom.Value);
            }


            if (options.CreatorId != null)
            {
              
                var user = userService_.GetUserById(options.CreatorId); 
                
                query = query.Where(c => user.Projects.Contains(c)); 
            }

            if (options.BackerId != null)
            {

                var user = userService_.GetUserById(options.CreatorId);

                query = query.Where(c => user.Projects.Contains(c));
            }

            if (options.RewardId != null)
            {
                query = query
                    .Where(c => c.AvailableRewards.Any(a => a.RewardId == options.RewardId.Value));
            }

            query = query.Take(500);
            return query;            
        }

        public Project GetProjectById(int? projectId)
        {
            if (projectId == null)
            {
                return null;
            }

            return SearchProjects(new SearchProjectOptions()
            {
                ProjectId = projectId
            }).Include(a=> a.RewardUsers).ThenInclude(a=> a.Reward).SingleOrDefault();                
        }

        public bool UpdateProject(UpdateProjectOptions options)
        {
            if (options == null)
            {
                return false; //Request anti gia false
            }

            var project = GetProjectById(options.ProjectId);

            if (project == null)
            {
                return false;
            }

            if (project.IsValidTitle(options.Title))
            {
                project.Title = options.Title;
            }

            if (project.IsValidCategory(options.Category))
            {
                project.Category = options.Category;
            }

            if (project.IsValidDeadline(options.Deadline))
            {
                project.Deadline = options.Deadline;
            }

            if (project.IsValidDescription(options.Description))
            {
                project.Description = options.Description;
            }

            if (project.IsValidTotalFund(options.TotalFund))
            {
                project.TotalFund = options.TotalFund;
            }

            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteProject(int? projectId)
        {
            var project = GetProjectById(projectId);
            context_.Remove(project);
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

            return SearchProjects(new SearchProjectOptions()
            {
                RewardId = rewardId
            })                    
                //.Include(a => a.AvailableRewards)
                .SingleOrDefault();      
        }

        public decimal? CalculateCurrentFund(Project project)
        {
           
            if (project == null)
            {
                return null;

            }

            decimal sum = 0m;

            foreach(var rw in project.RewardUsers)
            {
                sum = sum + rw.Quantity * rw.Reward.Price;
            }

            project.CurrentFund = sum;

            if (context_.SaveChanges() > 0)
            {
                return project.CurrentFund;
            }
            return null;
        }
    }
}
