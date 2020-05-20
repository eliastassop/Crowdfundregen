using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Services.Options
{
    public class UpdateStatusUpdateOptions
    {
        public int StatusUpdateId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
