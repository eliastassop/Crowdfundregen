﻿using Crowdfund.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund.Services
{
    public interface IUserService
    {
       User GetUserById(int? userId);
    }
}
