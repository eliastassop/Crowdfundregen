using Crowdfund.Models;
using Crowdfund.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Services
{
    public interface IProjectService
    {
        bool CreateProject(CreateProjectOptions options);
        bool UpdateProject(UpdateProjectOptions options);
        IQueryable<Project> SearchProjects(SearchProjectOptions options);
        Project GetProjectById(int? projectId);
        bool DeleteProject(int? projectId);
        Project GetProjectByRewardId(int? rewardId);
    }
}
