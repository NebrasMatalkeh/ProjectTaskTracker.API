using DataAccess.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using ProjectTaskTracker.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem>Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<MetaData>().HasNoKey();
            //modelBuilder.Entity<TaskItem>()
            //.Ignore(t => t.MetaData);
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Admain Manager",
                    Email = "admain@23.com",
                    PasswordHash = Convert.ToBase64String(Encoding.UTF8.GetBytes("Manager@345")),

                });
        }

    }
}