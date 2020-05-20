using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Models
{
    public class Media
    {
        public int MediaId { get; set; }
        public string MediaLink { get; set; }
        public MediaCategory Category { get; set; }

        public bool IsValidCategory(MediaCategory category)
        {
            return Enum.IsDefined(typeof(MediaCategory), category);
        }


    }
}
