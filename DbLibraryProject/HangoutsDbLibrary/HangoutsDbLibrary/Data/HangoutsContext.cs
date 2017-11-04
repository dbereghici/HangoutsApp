using HangoutsDbLibrary.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HangoutsDbLibrary.Data
{
    public class HangoutsContext : DbContext
    {
        public HangoutsContext(DbContextOptions<HangoutsContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Plan>().ToTable("Plan");
            //modelBuilder.Entity<Group>().ToTable("Group");
            modelBuilder.Entity<Activity>().ToTable("Activity");
            modelBuilder.Entity<Activity>().ToTable("Activity");
            modelBuilder.Entity<UserGroup>().ToTable("UserGroup");

            //Ignored Chat type
            modelBuilder.Ignore<Chat>();

            //Ignored User -> Fullname property
            modelBuilder.Entity<User>()
                .Ignore(u => u.Fullname);

            //GroupNr prop is set as PrimaryKey for Group
            modelBuilder.Entity<Group>()
                .HasKey(g => g.GroupNr);

            //Description prop is required for Activity
            modelBuilder.Entity<Activity>()
                .Property(a => a.Description)
                .IsRequired()
                .HasMaxLength(10);

            //One Group to Many Activities
            modelBuilder.Entity<Group>()
                .HasMany(g => g.Activities)
                .WithOne();

            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Group)
                .WithMany(g => g.Activities)
                .OnDelete(DeleteBehavior.Restrict);

            //One to one Group -> GroupAdmin
            modelBuilder.Entity<Group>()
                .HasOne(g => g.Admin)
                .WithOne(ga => ga.Group)
                .HasForeignKey<GroupAdmin>(g => g.GroupAdminForeignKey); //always specify a foreing key for one-to-one rel.

            //Many to many User -> Group(via UserGroup)
            modelBuilder.Entity<UserGroup>()
                .HasKey(ug => new { ug.UserId, ug.GroupId });

            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId);

            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId);

            //Inheritance
            modelBuilder.Entity<Activity>()
                .HasDiscriminator<string>("activity_type")
                .HasValue<Activity>("activity_base")
                .HasValue<SportsActivity>("activity_sports");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);
        }
    }
}
