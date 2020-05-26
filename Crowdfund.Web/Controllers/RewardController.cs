using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Data;
using Crowdfund.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfund.Web.Controllers
{
    [Route("project/{projectid}")]
    public class RewardController : Controller
    {

        private IUserService userService_;
        private IProjectService projectService_;
        private IRewardService rewardService_;
        private IRewardUserService rewardUserService_;
        private CrowdfundDB context;
        //private readonly ILogger<HomeController> _logger;


        public RewardController()
        {
            context = new CrowdfundDB();
            userService_ = new UserService(context);
            projectService_ = new ProjectService(context, userService_);
            rewardService_ = new RewardService(context, projectService_);
            rewardUserService_ = new RewardUserService(context, userService_, projectService_, rewardService_);
        }
        [HttpGet("rewards")]
        public IActionResult ShowAvailableRewards(int projectid)
        {
            var project = projectService_.GetProjectById(projectid).Data;

            return View(project.AvailableRewards);
        }
        public IActionResult BuyReward()
        {
            return View();
        }
        public IActionResult AddReward()
        {
            return View();
        }
        public IActionResult UpdateReward()
        {
            return View();
        }
    }
}