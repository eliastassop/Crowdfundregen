using Crowdfund.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdfund.Web.Models
{
    public class ExploreViewModel
    {
        public ProjectCategory Category { get; set; }

        public List<Project> ProjectList { get; set; }

        
    }
}
