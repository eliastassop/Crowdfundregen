using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crowdfund.Core.Data;
using Crowdfund.Core.Models;
using Crowdfund.Core.Services;
using Crowdfund.Core.Services.Options;
using Crowdfund.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace Crowdfund.Web.Controllers
{
    [Route("project")]
    public class ProjectController : Controller
    {
        private IUserService userService_;
        private IProjectService projectService_;
        private IRewardService rewardService_;
        private IRewardUserService rewardUserService_;
        private IStatusUpdateService statusUpdateService_;
        private CrowdfundDB context;
        //private readonly ILogger<HomeController> _logger;


        public ProjectController()
        {
            context = new CrowdfundDB();
            userService_ = new UserService(context);
            projectService_ = new ProjectService(context, userService_);
            rewardService_ = new RewardService(context, projectService_);
            rewardUserService_ = new RewardUserService(context, userService_, projectService_, rewardService_);
            statusUpdateService_ = new StatusUpdateService(context, projectService_);
        }
       
        
        
        [HttpPost("create")]
        public IActionResult CreateProjectDatabase([FromBody] CreateProjectOptions options)//landingpage
        {

            var result= projectService_.CreateProject(options);
            if (result.Success)
            {
                return Json(result.Data);
            }
            return StatusCode((int)result.ErrorCode, result.ErrorText);            
        }
        
        
        [HttpGet("{projectId}/projectinfo")]
        public IActionResult ProjectInfo(string projectId)//landingpage
        {

            var project = projectService_.GetProjectById(int.Parse(projectId));
            if (!project.Success)
            {
                return StatusCode((int)project.ErrorCode, project.ErrorText);
            }
            
            return View(project.Data);

        }
        public IActionResult ViewProjectRewards()//redirect
        {
            return View();
        }        
        public IActionResult ViewProjectStatusUpdates()//redirect
        {
            return View();
        }
        [HttpPost("{projectid}/updateproject")]
        public IActionResult UpdateProject(string projectid,[FromBody]UpdateProjectOptions options)
        {
            var projectid_ = int.Parse(projectid);
            var project = projectService_.UpdateProject(projectid_, options);
            return Json(project.Data);
        }

        [HttpGet("{projectId}/addreward")]
        public IActionResult AddReward(string projectId)
        {
            var project = projectService_.GetProjectById(int.Parse(projectId));
            //if (!project.Success)
            //{
            //    return StatusCode((int)project.ErrorCode, project.ErrorText);
            //}
            return View(project.Data);
        }
        public IActionResult UpdateReward()
        {
            return View();
        }
        public IActionResult DeleteReward()
        {
            return View();
        }

        [HttpGet("{projectId}/addmedia")]
        public IActionResult AddMedia(string projectId)
        {
            var project = projectService_.GetProjectById(int.Parse(projectId));
            //if (!project.Success)
            //{
            //    return StatusCode((int)project.ErrorCode, project.ErrorText);
            //}
            return View(project.Data);
        }        

        public IActionResult UpdateMedia()
        {
            return View();
        }
        public IActionResult DeleteMedia()
        {
            return View();
        }

        [HttpGet("{projectId}/addstatusupdate")]
        public IActionResult AddStatusUpdate(string projectId)
        {
            var project = projectService_.GetProjectById(int.Parse(projectId));
            //if (!project.Success)
            //{
            //    return StatusCode((int)project.ErrorCode, project.ErrorText);
            //}
            return View(project.Data);
        }

        [HttpGet("{statusUpdateId}/updatestatusupdate")]
        public IActionResult UpdateStatusUpdate(string statusUpdateId)
        {
            var statusUpdate = statusUpdateService_.GetStatusUpdateById(int.Parse(statusUpdateId));
            //if (!project.Success)
            //{
            //    return StatusCode((int)project.ErrorCode, project.ErrorText);
            //}
            return View(statusUpdate.Data);
        }
        [HttpGet("{text}/searchbytext")] /*{text}/*/
        public IActionResult SearchProjectsByText(string text)
        {
            var listprojects = projectService_.SearchProjects(new SearchProjectOptions { SearchText = text }).ToList();
            var project = new SearchByTextViewModel()
            {
                Text = text,
                ProjectList = listprojects

            };
            return View(project);
        }
     
    }
}