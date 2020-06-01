using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Data;
using Crowdfund.Core.Services;
using Crowdfund.Core.Services.Options;
using Crowdfund.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Crowdfund.Web.Controllers
{
    [Route("user")] // isws dhmiourgisei provlima
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
        [HttpPost]
        public IActionResult LogInUsernameId([FromBody]string username)
        {
            var userid = userService_.GetIdByUserName(username);
            if (userid.Success)
            {
                return Json(userid.Data);
            }
            return StatusCode((int)userid.ErrorCode,userid.ErrorText);
        }

        [HttpGet("{id}/userpersonalinfo")]
        public IActionResult UserPersonalInfo(string id)
        {
            var userId = int.Parse(id);
            var user = userService_.GetUserById(userId);
            var usermodel = new UserProjectsBacked()
            {
                User = user.Data,
                ProjectsBacked = rewardUserService_.SearchProjectsFundedByUser(userId).ToList()

            };

            if (usermodel.User == null)
            {
                return StatusCode((int)user.ErrorCode,user.ErrorText);
            }            

            return View(usermodel);
           
        }
        
        
       
       
        [HttpPost("create")]
        public IActionResult CreateUser([FromBody] CreateUserOptions options) 
        {
            
            var user = userService_.CreateUser(options);
            if (user.Success)
            {
                return Json(user.Data);
            }
            return StatusCode((int)user.ErrorCode, user.ErrorText);
            

        }

        [HttpPost("{id}/updateuserpersonalinfo")]
        public IActionResult UpdateUserPersonalInfo(string id, [FromBody] UpdateUserOptions options)
        {
            var userId = int.Parse(id);
            var update = userService_.UpdateUser(userId,options);
            return Json(update.Data);
            
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