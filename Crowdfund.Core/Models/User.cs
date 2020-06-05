using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Core.Models
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
            return !string.IsNullOrWhiteSpace(username) && username.Length <= 10;
        }
        public bool IsValidEmail(string email)//email validation checkonline
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            email = email.Trim();

            if (email.Contains("@"))
            {
                var count = email.Count(x => x == '@');
                if (count == 1)
                {
                    return true;
                } 
                
            }
            return false;
        }
    }
}
