using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Data;
using Crowdfund.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Crowdfund.Web.Controllers
{
    [Route("user")]
    public class UserController : Controller
    {
        private IUserService userService_;
        private IProjectService projectService_;
        private IRewardService rewardService_;
        private IRewardUserService rewardUserService_;
        private CrowdfundDB context;
        //private readonly ILogger<HomeController> _logger;


        public UserController()
        {
            context = new CrowdfundDB();
            userService_ = new UserService(context);
            projectService_ = new ProjectService(context, userService_);
            rewardService_ = new RewardService(context, projectService_);
            rewardUserService_ = new RewardUserService(context, userService_, projectService_, rewardService_);
        }
        
        [HttpGet("{id}")]
        public IActionResult UserPersonalInfo()
        {
            var user = userService_.GetUserById(1).Data;
            return Json(user);
        }
        [HttpGet("{id}/edit")]
        public IActionResult UpdateUserPersonalInfo()
        {
            return View();
        }
        public IActionResult ProjectsBacked()
        {
            return View();
        }
        public IActionResult ProjectsCreated()
        {
            return View();
        }
    }
}