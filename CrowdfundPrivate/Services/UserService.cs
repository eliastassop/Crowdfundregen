using Crowdfund.Data;
using Crowdfund.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Services
{
    public class UserService:IUserService
    {
        private CrowdfundDB context_;
        public UserService(CrowdfundDB context)
        {
            context_ = context;            
        }
        public User GetUserById(int? userId)
        {
            if (userId == null)
            {
                return null;
            }
            var user = context_
                 .Set<User>()
                 .AsQueryable()
                 .Where(c => c.UserId == userId)
                 .SingleOrDefault();
            return user;            
        }
    }
}
