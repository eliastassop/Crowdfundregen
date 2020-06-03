using Crowdfund.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crowdfund.Web.Models
{
    public class SearchByTextViewModel
    {
        public string Text { get; set; }

        public List<Project> ProjectList { get; set; }


    }
}
