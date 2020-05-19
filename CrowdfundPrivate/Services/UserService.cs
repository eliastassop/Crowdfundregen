using Crowdfund.Data;
using Crowdfund.Models;
using Crowdfund.Services.Options;
using Microsoft.EntityFrameworkCore;
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
        public bool CreateUser(CreateUserOptions options)
        {
            if (options == null)
            {
                return false;
            }

            
            var user = new User()
            {
                UserName = options.UserName,
                Email = options.Email,
            };
            // check valid entries
            if (!user.IsValidUsername(options.UserName)
                ||!user.IsValidUsername(options.Email))
            {
                return false;
            }


            //check duplicate
            if (!CheckDuplicates(options.Email, options.UserName))
            {
                return false;
            }
                        
            context_.Add(user);

            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;

            
            
        }
        public bool DeleteUser(int? userId)
        {
            if (userId == null)
            {
                return false;
            }
            var user = GetUserById(userId);
            context_.Remove(user);
            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }
        public IQueryable<User> SearchUser(SearchUserOptions options)
        {
            if (options == null)
            {
                return null;
            }
            var query = context_
                .Set<User>()
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(options.UserName))
            {
                query = query.Where(c => c.UserName.Contains(options.UserName));
            }

            if (!string.IsNullOrWhiteSpace(options.Email))
            {
                query = query.Where(c => c.Email == options.Email);
            }

            if (options.UserId != null)
            {
                query = query.Where(c => c.UserId == options.UserId);
            }

            if (options.UserCreatedFrom != null)
            {
                query = query.Where(c => c.UserCreated >= options.UserCreatedFrom);
            }
            if (options.UserCreatedTo != null)
            {
                query = query.Where(c => c.UserCreated < options.UserCreatedTo);
            }

            query = query.Take(500);
            return query;
        }
        public User GetUserById(int? userId)
        {
            if (userId == null)
            {
                return null;
            }
            //var user = context_
            //     .Set<User>()
            //     .AsQueryable()
            //     .Where(c => c.UserId == userId)
            //     .Include(a => a.Projects)
            //     .SingleOrDefault();

            return SearchUser(new SearchUserOptions()
            {
                UserId = userId
            }).Include(c => c.Projects)
              .SingleOrDefault();
        }
        public bool CheckDuplicates(string email, string userName) 
        {
            if (SearchUser(new SearchUserOptions()
            {
                Email = email,
                UserName = userName,
            }).Any())
            {
                return false;
            }
            return true;

        }
        public bool UpdateUser(UpdateUserOptions options)
        {
            if (options == null)
            {
                return false;
            }
            var user = GetUserById(options.UserId);
            if (user == null)
            {
                return false;
            }

            if (!CheckDuplicates(options.Email, options.UserName)) 
            { 
                return false;
            }
            
            if (user.IsValidUsername(options.UserName))
            {
                user.UserName = options.UserName;
            }
            if (user.IsValidUsername(options.Email))
            {
                user.Email = options.Email;
            }
            if (context_.SaveChanges() > 0)
            {
                return true;
            }
            return false;
        }

     
    }
    
}
