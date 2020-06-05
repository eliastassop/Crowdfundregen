using Crowdfund.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdfund.Web.Models
{
    public class TrendingRecentProjects
    {
        public List<Project> TrendingProjects { get; set; }
        public List<Project> LatestProjects { get; set; }
    }
}
