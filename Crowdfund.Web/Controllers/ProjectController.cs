﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Data;
using Crowdfund.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Crowdfund.Web.Controllers
{
    [Route("project")]
    public class ProjectController : Controller
    {
        private IUserService userService_;
        private IProjectService projectService_;
        private IRewardService rewardService_;
        private IRewardUserService rewardUserService_;
        private CrowdfundDB context;
        //private readonly ILogger<HomeController> _logger;


        public ProjectController()
        {
            context = new CrowdfundDB();
            userService_ = new UserService(context);
            projectService_ = new ProjectService(context, userService_);
            rewardService_ = new RewardService(context, projectService_);
            rewardUserService_ = new RewardUserService(context, userService_, projectService_, rewardService_);
        }        
        public IActionResult ViewProjectDescription()//landingpage
        {
            return View();
        }
        public IActionResult ViewProjectRewards()//redirect
        {
            return View();
        }        
        public IActionResult ViewProjectStatusUpdates()//redirect
        {
            return View();
        }
        public IActionResult UpdateProject()
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
        public IActionResult DeleteReward()
        {
            return View();
        }
        public IActionResult AddMedia()
        {
            return View();
        }
        public IActionResult UpdateMedia()
        {
            return View();
        }
        public IActionResult DeleteMedia()
        {
            return View();
        }
    }
}