using Crowdfund.Core.Models;
using Crowdfund.Core.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Core.Services
{
    public interface IUserService
    {
        Result<User> CreateUser(CreateUserOptions options);
        Result<bool> UpdateUser(int userId,UpdateUserOptions options);
        Result<bool> DeleteUser(int userId);
        IQueryable<User> SearchUser(SearchUserOptions options);
        Result<User> GetUserById(int userId);
        Result<int> GetIdByUserName(string username);
        bool IsDuplicate(string email, string userName);
        //IQueryable SearchFundedProjectsByUser();

    }
}
