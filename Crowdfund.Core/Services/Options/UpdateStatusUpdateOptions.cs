using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Services.Options
{
    public class UpdateStatusUpdateOptions
    {        
        public int StatusUpdateId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
