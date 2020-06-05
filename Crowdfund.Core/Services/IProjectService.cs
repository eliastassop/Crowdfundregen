using Crowdfund.Core.Models;
using Crowdfund.Core.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Core.Services
{
    public interface IProjectService
    {
        Result<Project> CreateProject(CreateProjectOptions options);
        Result<bool> UpdateProject(int projectId,UpdateProjectOptions options);
        Result<bool> DeleteProject(int projectId);
        IQueryable<Project> SearchProjects(SearchProjectOptions options);
        Result<Project> GetProjectById(int projectId);
        Result<Project> GetProjectByRewardId(int rewardId);
        Result<bool> UpdateCurrentFund(Project project);
        IQueryable<Project> SearchTrendingProjects();
        IQueryable<Project> SearchLatestProjects();
    }
}
