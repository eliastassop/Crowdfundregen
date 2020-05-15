using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Models
{
    public class StatusUpdate
    {
        public int StatusUpdateId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UpdateCreated { get; set; }        
    }
    
}
