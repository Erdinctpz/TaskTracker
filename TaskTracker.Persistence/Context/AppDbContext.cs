using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TaskTracker.Domain;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Persistence.Context
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUserGroup>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId);

            builder.Entity<AppUserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId);

            builder.Entity<TodoItem>()
                .HasOne(t => t.TaskGroup)
                .WithMany(g => g.Tasks)
                .HasForeignKey(t => t.TaskGroupId)
                .OnDelete(DeleteBehavior.SetNull);
        }
        #region TABLES

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TaskGroup> TaskGroups { get; set; }
        public DbSet<AppUserGroup> AppUserGroups { get; set; }

        #endregion
    }

}