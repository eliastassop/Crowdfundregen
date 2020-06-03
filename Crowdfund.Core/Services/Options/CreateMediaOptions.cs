using Crowdfund.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Services.Options
{
    public class CreateMediaOptions
    {
        public int ProjectId { get; set; }
        public string MediaLink { get; set; }
        public string Category { get; set; }
    }
}
