using Crowdfund.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace Crowdfund.Core.Data
{
    public class CrowdfundDB : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=localhost;Database=Crowdfund;User Id=sa;Password=admin!@#123;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder
              .Entity<User>()
              .ToTable("User");

            modelBuilder
               .Entity<Project>()
               .ToTable("Project");

            modelBuilder
                .Entity<Reward>()
                .ToTable("Reward");

            modelBuilder
                .Entity<RewardUser>()
                .ToTable("RewardUser");
            modelBuilder
                .Entity<StatusUpdate>()
                .ToTable("StatusUpdate");
            modelBuilder
                .Entity<Media>()
                .ToTable("Media");

            modelBuilder
               .Entity<RewardUser>()
               .HasKey(rb => new { rb.UserId, rb.RewardId });
        }

    }
}
