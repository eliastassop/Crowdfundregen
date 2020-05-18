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
            //using var Crwdfunddb = new CrowdfundDB();
            //{
            //var user = new User()
            //{
            //    UserName = "spyros",
            //    Email = "spyros@gmail.com",
            //};

            //Crwdfunddb.Add(user);

            //user = new User()
            //{
            //    UserName = "alexandros",
            //    Email = "alexadros@gmail.com",
            //};
            //var user = new User()
            //{
            //    UserName = "elias",
            //    Email = "elias@gmail.com",
            //};


            //Crwdfunddb.Add(user);
            //Crwdfunddb.SaveChanges();

            ////get data 
            //var customer2 = tinyCrmDbContext
            //    .Set<Customer>()
            //    .Where(c => c.Email == "gdoiko@mail.com")
            //    .FirstOrDefault();

            ////update data
            //customer2.VatNumber = "012345678";
            //tinyCrmDbContext.SaveChanges();

            //var user3 = Crwdfunddb
            //    .Set<User>()
            //    .Where(c => c.UserId == 3)
            //    .SingleOrDefault();
            //var project = new Project()
            //{

            //    Title = "Second PROJECT",
            //    Description = "Project ipa",
            //    Category = ProjectCategory.Comics,
            //    Deadline = new DateTime(2020, 10, 21),
            //    TotalFund = 40,
            //};

            //project.AvailableRewards.Add(new Reward()
            //{
            //    Title = "First Reward of 2nd project",
            //    Description = "2nd Beta Version",
            //    Price = 300,

            //});
            //project.AvailableRewards.Add(new Reward()
            //{
            //    Title = "Second Reward of 2nd project",
            //    Description = "2nd Full Version",
            //    Price = 600,
            //});

            //user3.Projects.Add(project);
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
            //   .Where(c => c.ProjectId == 1)
            //   .SingleOrDefault();
            //        var reward1 = Crwdfunddb
            //           .Set<Reward>()
            //           .Where(c => c.RewardId == 4)
            //           .SingleOrDefault();
            //        var rewarduser = new RewardUser()
            //        {
            //            Quantity = 10,
            //            User = user1,
            //            Reward = reward1
            //        };
            //        project.RewardUsers.Add(rewarduser);
            //        Crwdfunddb.SaveChanges();
            //    }
            using (var context = new CrowdfundDB())
            {
                IUserService userService = new UserService(context);
            
                IProjectService projectService = new ProjectService(context, userService);

                //var success = projectService.CreateProject(new CreateProjectOptions()
                //{
                //    CreatorId = 1,
                //    Category = ProjectCategory.Dance,
                //    Deadline = new DateTime(2020, 4, 4),
                //    Description = "my new dance moves",
                //    Title = "Third Project",
                //    TotalFund = 40,

                //});

                var doublesuccess = projectService.SearchProjects(new SearchProjectOptions()
                {
                    CreatorId = 1,
                }).ToList();
            }
           
        }
    }
}
