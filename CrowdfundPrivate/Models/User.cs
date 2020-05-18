using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Models
{
    public class User // can be projectcreator and backer.
    {
        public int UserId { get; set; }
        public string UserName { get;  set; }
        public DateTime UserCreated { get; set; }
        public List<Project> Projects { get; set; }
        
        public string Email { get; set; }
        
        
        public User()
        {
            Projects = new List<Project>();
            UserCreated = DateTime.Now;
        }

        public bool IsValidUsername(string username)
        {
            return string.IsNullOrWhiteSpace(username) && username.Length <= 30;
        }
    }
}
