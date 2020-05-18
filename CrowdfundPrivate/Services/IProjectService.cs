using Crowdfund.Models;
using Crowdfund.Services.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Services
{
    public interface IProjectService
    {
        public bool CreateProject(CreateProjectOptions options);
        public bool UpdateProject(UpdateProjectOptions options);
    }
}
