using Crowdfund.Core.Data;
using Crowdfund.Core.Models;
using Crowdfund.Core.Services.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Core.Services
{
    public class UserService:IUserService
    {
        private CrowdfundDB context_;
        public UserService(CrowdfundDB context)
        {
            context_ = context;            
        }
        public Result<User> CreateUser(CreateUserOptions options)
        {
            if (options == null)
            {
                return Result<User>.CreateFailed(StatusCode.BadRequest, "Null options");
            }

            var user = new User()
            {
                UserName = options.UserName,
                Email = options.Email,
            };
            // check valid entries
            if (!user.IsValidUsername(options.UserName)
                ||!user.IsValidEmail(options.Email))
            {
                return Result<User>.CreateFailed(StatusCode.BadRequest, "Please check the Username or Email options");
            }


            //check duplicate
            if (!CheckDuplicates(options.Email, options.UserName))
            {
                return Result<User>.CreateFailed(StatusCode.BadRequest,"The username or Email already exists");
            }
                        
            context_.Add(user);

            if (context_.SaveChanges() <= 0)
            {
                return Result<User>.CreateFailed(
                    StatusCode.InternalServerError,
                    "User could not be created");
            }

            return Result<User>.CreateSuccessful(user);

        }
        public Result<bool> DeleteUser(int userId)
        {            
            var user = GetUserById(userId).Data;

            if (user == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, $"User with {userId} was not found");
            }

            context_.Remove(user);

            if (context_.SaveChanges() == 0)
            {
                return Result<bool>.CreateFailed(StatusCode.InternalServerError, "Something went wrong");
            }

            return Result<bool>.CreateSuccessful(true);


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
        public Result<User> GetUserById(int userId)
        {
            
            //var user = context_
            //     .Set<User>()
            //     .AsQueryable()
            //     .Where(c => c.UserId == userId)
            //     .Include(a => a.Projects)
            //     .SingleOrDefault();
            
            var user= SearchUser(new SearchUserOptions()
            {
                UserId = userId
            }).Include(c => c.Projects)
              .SingleOrDefault();
            if (user == null)
            {
                Result<User>.CreateFailed(StatusCode.NotFound, "No such user exists");
            }
            return Result<User>.CreateSuccessful(user);
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
        public Result<bool> UpdateUser(int userId,UpdateUserOptions options)
        {
            if (options == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, "Null options");
            }
            
            var user = GetUserById(userId).Data;
            
            if (user == null)
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, $"User with {userId} was not found");
            }

            if (!CheckDuplicates(options.Email, options.UserName)) 
            {
                return Result<bool>.CreateFailed(StatusCode.BadRequest, "The username or Email already exists");
            }
            
            if (user.IsValidUsername(options.UserName))
            {
                user.UserName = options.UserName;
            }

            if (user.IsValidUsername(options.Email))
            {
                user.Email = options.Email;
            }
            

            if (context_.SaveChanges() == 0)
            {
                return Result<bool>.CreateFailed(StatusCode.InternalServerError, "Something went wrong");
            }

            return Result<bool>.CreateSuccessful(true);
        }

     
    }
    
}
