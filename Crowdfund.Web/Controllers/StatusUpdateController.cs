using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Data;
using Crowdfund.Core.Services;
using Crowdfund.Core.Services.Options;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfund.Web.Controllers
{
    [Route("statusupdate")]
    public class StatusUpdateController : Controller
    {
        private IUserService userService_;
        private IProjectService projectService_;
        private IRewardService rewardService_;
        private IMediaService mediaService_;
        private IStatusUpdateService statusUpdateService_;
        private IRewardUserService rewardUserService_;
        private CrowdfundDB context;
        //private readonly ILogger<HomeController> _logger;

        public StatusUpdateController()
        {
            context = new CrowdfundDB();
            userService_ = new UserService(context);
            projectService_ = new ProjectService(context, userService_);
            mediaService_ = new MediaService(context, projectService_);
            statusUpdateService_ = new StatusUpdateService(context, projectService_);
            rewardService_ = new RewardService(context, projectService_);
            rewardUserService_ = new RewardUserService(context, userService_, projectService_, rewardService_);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult CreateStatusUpdate([FromBody] CreateStatusUpdateOptions options)//landingpage
        {

            var result = statusUpdateService_.CreateStatusUpdate(options);
            if (result.Success)
            {
                return Json(result.Data);
            }
            return StatusCode((int)result.ErrorCode, result.ErrorText);
        }

        [HttpPatch("update")]
        public IActionResult UpdateStatusUpdate([FromBody] UpdateStatusUpdateOptions options)//landingpage
        {

            var result = statusUpdateService_.UpdateStatusUpdate(options);
            if (result.Success)
            {
                return Json(result.Data);
            }
            return StatusCode((int)result.ErrorCode, result.ErrorText);
        }
    }
}