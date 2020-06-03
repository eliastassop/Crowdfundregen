using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Data;
using Crowdfund.Core.Services;
using Crowdfund.Core.Services.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Crowdfund.Web.Controllers
{
    [Route("rewarduser")]
    public class RewardUserController : Controller
    {
        private IUserService userService_;
        private IProjectService projectService_;
        private IRewardService rewardService_;
        private IRewardUserService rewardUserService_;
        private CrowdfundDB context;
        //private readonly ILogger<HomeController> _logger;


        public RewardUserController(ILogger<RewardUserController> logger)
        {
            context = new CrowdfundDB();
            userService_ = new UserService(context);
            projectService_ = new ProjectService(context, userService_);
            rewardService_ = new RewardService(context, projectService_);
            rewardUserService_ = new RewardUserService(context, userService_, projectService_, rewardService_);

        }

        [HttpPost("createOrupdateRewardUser")]
        public IActionResult CreateOrUpdateRewardUser([FromBody]CreateRewardUserOptions options)
        {
            var rewardUser = rewardUserService_.CreateOrUpdateRewardUser(options);
            if (rewardUser.Success)
            {
                return Json(rewardUser.Data);
            }
            return StatusCode((int)rewardUser.ErrorCode, rewardUser.ErrorText);
        }
    }
}