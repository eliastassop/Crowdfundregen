using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Models
{
    public class StatusUpdate
    {
        public int StatusUpdateId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UpdateCreated { get; set; }   
        

        public StatusUpdate()
        {
            UpdateCreated = DateTime.Now;
        }

        public bool IsValidTitle(string title)
        {
            return !string.IsNullOrWhiteSpace(title) && title.Length <= 50;
        }

        public bool IsValidDescription(string description)
        {
            return !string.IsNullOrWhiteSpace(description) && description.Length <= 10000;
        }
    }
    
}
