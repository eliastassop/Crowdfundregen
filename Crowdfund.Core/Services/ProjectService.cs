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
    public class ProjectService:IProjectService
    {
        private CrowdfundDB context_;
        private IUserService userService_;
        
        public ProjectService(CrowdfundDB context, IUserService userService)
        {
            context_ = context;
            userService_ = userService;            
        }
        
        public Result<Project> CreateProject(CreateProjectOptions options)
        {
            if (options == null)
            {
                return Result<Project>.CreateFailed(StatusCode.BadRequest, "Null options");
            }
            
            var user = userService_.GetUserById(options.CreatorId);
            if (user == null)
            {
                return Result<Project>.CreateFailed(StatusCode.BadRequest, $"User with {options.CreatorId} was not found");
            }


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
                return Result<Project>.CreateFailed(StatusCode.BadRequest, "Please check the validations");

            }
            //validation prin mpei sti vasi
            user.Projects.Add(project);
            //context_.Add(user);
            if (context_.SaveChanges() <= 0)
            {
                return Result<Project>.CreateFailed(
                    StatusCode.InternalServerError,
                    "Project could not be created");
            }

            return Result<Project>.CreateSuccessful(project);

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
              
                var user = userService_.GetUserById(options.CreatorId.Value); 
                
                query = query.Where(c => user.Projects.Contains(c)); 
            }

            if (options.BackerId != null)
            {

                var user = userService_.GetUserById(options.CreatorId.Value);

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

        public Project GetProjectById(int projectId)
        {

            return SearchProjects(new SearchProjectOptions()
            {
                ProjectId = projectId
            }).Include(a=> a.RewardUsers)
            .ThenInclude(a=> a.Reward)
            .SingleOrDefault();                
        }

        public Result<bool> UpdateProject(int projectId,UpdateProjectOptions options)
        {
            if (options == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, "Null options");
            }

            var project = GetProjectById(projectId);

            if (project == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, $"Project with {projectId} was not found");
            }

            if (project.IsValidTitle(options.Title))
            {
                project.Title = options.Title;
            }
            //else
            {
               // return Result<bool>.UpdateFailed(StatusCode.BadRequest, "The Tittle is not valid");

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

            if (context_.SaveChanges() == 0)
            {
                return Result<bool>.CreateFailed(StatusCode.InternalServerError, "Something went wrong");
            }

            return Result<bool>.CreateSuccessful(true);
        }

        public Result<bool> DeleteProject(int projectId)
        {
            
            var project = GetProjectById(projectId);
            if (project == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, $"Project with {projectId} was not found");
            }

            context_.Remove(project);
            if (context_.SaveChanges() == 0)
            {
                return Result<bool>.CreateFailed(StatusCode.InternalServerError, "Something went wrong");
            }

            return Result<bool>.CreateSuccessful(true);
        }

        public Project GetProjectByRewardId(int rewardId)
        {
            return SearchProjects(new SearchProjectOptions()
            {
                RewardId = rewardId
            })
                //.Include(a => a.AvailableRewards)
                .Include(a => a.RewardUsers)
                .ThenInclude(a => a.Reward)
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
