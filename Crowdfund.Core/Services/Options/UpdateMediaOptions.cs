using Crowdfund.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Services.Options
{
    public class UpdateMediaOptions
    {        
        public string MediaLink { get; set; }
        public MediaCategory Category { get; set; }
    }
}
