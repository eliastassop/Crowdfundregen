using Crowdfund.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdfund.Web.Models
{
    public class UserProjectsBacked
    {
        public User User { get; set; }

        public List<Project> ProjectsBacked { get; set; }

        public UserProjectsBacked()
        {
            ProjectsBacked = new List<Project>();
        }
    }
}
