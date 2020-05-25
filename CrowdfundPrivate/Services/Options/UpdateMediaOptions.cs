using Crowdfund.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Services.Options
{
    public class UpdateMediaOptions
    {        
        public string MediaLink { get; set; }
        public MediaCategory Category { get; set; }
    }
}
