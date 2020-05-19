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
        bool CreateUser(CreateUserOptions options);
        IQueryable<User> SearchUser(SearchUserOptions options);
        User GetUserById(int? userId);
        bool DeleteUser(int? userid);
        bool UpdateUser(UpdateUserOptions options);
        bool CheckDuplicates(string email, string userName);
        //IQueryable SearchFundedProjectsByUser();

    }
}
