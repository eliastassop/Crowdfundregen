using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Models
{
    public class ProjectCreator
    {
        int ProjectCreatorId { get; set; }
        string UserName { get; set; }
        DateTime UserCreated { get; set; }
        List<Project> Projects { get; set; }

        public ProjectCreator()
        {
            Projects = new List<Project>();
        }
    }
}
