using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Services.Options
{
    public class SearchUserOptions
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public DateTime? UserCreatedFrom { get; set; }
        public DateTime? UserCreatedTo { get; set; }
        public string Email { get; set; }

    }
}
