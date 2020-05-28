using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Crowdfund.Web.Models;
using Crowdfund.Core.Data;
using Crowdfund.Core.Services;
using Crowdfund.Core.Services.Options;
using Crowdfund.Core.Models;

namespace Crowdfund.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService userService_;
        private IProjectService projectService_;
        private IRewardService rewardService_;
        private IRewardUserService rewardUserService_;
        private CrowdfundDB context;
        //private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            context = new CrowdfundDB();
            userService_ = new UserService(context);
            projectService_ = new ProjectService(context, userService_);
            rewardService_ = new RewardService(context, projectService_);
            rewardUserService_ = new RewardUserService(context, userService_, projectService_, rewardService_);

        }

        public IActionResult Index()
        {
            //var user = userService_.GetUserById(1);
            //var currentFund2 = projectService_.GetProjectById(1);
            //if (!currentFund2.Success)
            //{
            //    return StatusCode((int)currentFund2.ErrorCode, currentFund2.ErrorText);
            //}
            return View();
        }

        public IActionResult TrendingProjects()
        {
            return View();
        }
        public IActionResult Explore(ProjectCategory category)
        {
            var test = projectService_.SearchProjects(new SearchProjectOptions { Category = category }).ToList();

            return Json(test);
        }
        public IActionResult SearchProjects()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
           
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult LogIn() 
        {
            return View();

        }
        public IActionResult LogOut() 
        {
            return View();
        }
        
    }
}
