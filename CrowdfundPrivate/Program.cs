using Crowdfund.Models;
using System;

namespace Crowdfund
{
     class Program
    {
        static void Main(string[] args)
        {
            var prjctcret = new User()
            {

                UserName = "elias",

            };
            var prjct = new Project() { 
                Title = "test"
            };

            var rwrd = new Reward() {
                Title = " reward test ",
                RewardId = 1,
                
            
            };

           
           

            prjctcret.Projects.Add(prjct);
            prjct.AvailableRewards.Add(rwrd);

            var stst = new StatusUpdate();
            
            


        }
    }
}
