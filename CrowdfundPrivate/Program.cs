using Crowdfund.Data;
using Crowdfund.Models;
using Crowdfund.Services;
using Crowdfund.Services.Options;
using System;
using System.Linq;

namespace Crowdfund
{
    class Program
    {
        static void Main(string[] args)
        {
            //insert
            using var Crwdfunddb = new CrowdfundDB();
            {
                //var user = new User()
                //{
                //    UserName = "spyros",
                //    Email = "spyros@gmail.com",
                //};

                //Crwdfunddb.Add(user);


                //var user = new User()
                //{
                //    UserName = "alexandros",
                //    Email = "alexadros@gmail.com",
                //};
                //var user2 = new User()
                //{
                //    UserName = "elias",
                //    Email = "elias@gmail.com",
                //};


                //Crwdfunddb.Add(user);
                //Crwdfunddb.Add(user2);
                //Crwdfunddb.SaveChanges();

                ////get data 

                //var user2 = Crwdfunddb
                //    .Set<User>()
                //    .Where(c => c.UserId == 2)
                //    .SingleOrDefault();
                //var project = new Project()
                //{

                //    Title = "Fourth PROJECT",
                //    Description = "Project cgnbkc",
                //    Category = ProjectCategory.Crafts,
                //    Deadline = new DateTime(2020, 11, 21),
                //    TotalFund = 4000,
                //};

                //project.AvailableRewards.Add(new Reward()
                //{
                //    Title = "First Reward of 4 project",
                //    Description = " Beta Version",
                //    Price = 150,

                //});
                //project.AvailableRewards.Add(new Reward()
                //{
                //    Title = "Second Reward of 4 project",
                //    Description = " Full Version",
                //    Price = 250,
                //});

                //user2.Projects.Add(project);
                //Crwdfunddb.SaveChanges();

                //var user1 = Crwdfunddb
                //    .Set<User>()
                //    .Where(c => c.UserId == 1)
                //    .SingleOrDefault();


                //var user2 = Crwdfunddb
                //   .Set<User>()
                //   .Where(c => c.UserId == 2)
                //   .SingleOrDefault();
                //var project = Crwdfunddb
                //   .Set<Project>()
                //   .Where(c => c.ProjectId == 2)
                //   .SingleOrDefault();
                //var reward1 = Crwdfunddb
                //   .Set<Reward>()
                //   .Where(c => c.RewardId == 3)
                //   .SingleOrDefault();
                //var rewarduser = new RewardUser()
                //{
                //    Quantity = 2,
                //    User = user2,
                //    Reward = reward1
                //};
                //project.RewardUsers.Add(rewarduser);
                //Crwdfunddb.SaveChanges();

                //var success = projectService.CreateProject(new CreateProjectOptions()
                //{
                //    CreatorId = 1,
                //    Category = ProjectCategory.Dance,
                //    Deadline = new DateTime(2020, 4, 4),
                //    Description = "my new dance moves",
                //    Title = "Third Project",
                //    TotalFund = 40,

                //});

                //var doublesuccess = projectService.SearchProjects(new SearchProjectOptions()
                //{
                //    CreatorId = 1,
                //}).ToList();


                using (var context = new CrowdfundDB())
                {
                    IUserService userService = new UserService(context);
                    IProjectService projectService = new ProjectService(context, userService);
                    IRewardService rewardService = new RewardService(context, projectService);
                    IRewardUserService rewardUserService = new RewardUserService(context, userService, projectService, rewardService);

                    var test = rewardUserService.DeleteRewardUser(3,1);
                   

                    
                    //var test2 = projectService.CalculateCurrentFund(projectService.GetProjectById(1));
                  



                }





                

            }
        }
    }
}
