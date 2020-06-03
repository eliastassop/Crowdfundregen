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
    [Route("media")]
    public class MediaController : Controller
    {
        private IUserService userService_;
        private IProjectService projectService_;
        private IRewardService rewardService_;
        private IMediaService mediaService_;
        private IRewardUserService rewardUserService_;
        private CrowdfundDB context;
        //private readonly ILogger<HomeController> _logger;

        public MediaController()
        {
            context = new CrowdfundDB();
            userService_ = new UserService(context);
            projectService_ = new ProjectService(context, userService_);
            mediaService_ = new MediaService(context, projectService_);
            rewardService_ = new RewardService(context, projectService_);
            rewardUserService_ = new RewardUserService(context, userService_, projectService_, rewardService_);
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("create")]
        public IActionResult CreateMedia([FromBody] CreateMediaOptions options)//landingpage
        {

            var result = mediaService_.CreateMedia(options);
            if (result.Success)
            {
                return Json(result.Data);
            }
            return StatusCode((int)result.ErrorCode, result.ErrorText);
        }
    }
}