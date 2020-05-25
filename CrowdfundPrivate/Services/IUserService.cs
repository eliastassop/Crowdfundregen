using Crowdfund.Models;
using Crowdfund.Services.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crowdfund.Services
{
    public interface IUserService
    {
        Result<User> CreateUser(CreateUserOptions options);
        Result<bool> UpdateUser(int userId,UpdateUserOptions options);
        Result<bool> DeleteUser(int userId);
        IQueryable<User> SearchUser(SearchUserOptions options);
        User GetUserById(int userId);
        bool CheckDuplicates(string email, string userName);
        //IQueryable SearchFundedProjectsByUser();

    }
}
