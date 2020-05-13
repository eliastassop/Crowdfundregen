using Crowdfund.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crowdfund
{
    public class CrowdfundDB: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=localhost;Database=tinycrm;User Id=sa;Password=Password_01;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Backer>()
                .ToTable("Backer");

            modelBuilder
                .Entity<Project>()
                .ToTable("Project");

            modelBuilder
               .Entity<ProjectCreator>()
               .ToTable("ProjectCreator");

            modelBuilder
                .Entity<Reward>()
                .ToTable("Reward");

            modelBuilder
                .Entity<RewardBacker>()
                .ToTable("RewardBacker");

        }


    }
}
