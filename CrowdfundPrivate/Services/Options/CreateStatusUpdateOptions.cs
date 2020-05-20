using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Services.Options
{
    public class CreateStatusUpdateOptions
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
