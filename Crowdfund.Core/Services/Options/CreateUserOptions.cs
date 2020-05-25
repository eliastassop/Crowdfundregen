using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Core.Services.Options
{
    public class CreateUserOptions
    {
        public string UserName { get; set; }
        public DateTime UserCreated { get; set; }
        public string Email { get; set; }


    }
}
