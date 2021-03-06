﻿using HangoutsDbLibrary.Model;
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
        public HangoutsContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<UserChat> UserChats { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Group>().ToTable("Group");
            modelBuilder.Entity<UserGroup>().ToTable("UserGroup");
            modelBuilder.Entity<Friendship>().ToTable("Friendship");
            modelBuilder.Entity<Activity>().ToTable("Activity");
            modelBuilder.Entity<Plan>().ToTable("Plan");
            modelBuilder.Entity<PlanUser>().ToTable("PlanUser");
            modelBuilder.Entity<Chat>().ToTable("Chat");
            modelBuilder.Entity<Message>().ToTable("Message");
            modelBuilder.Entity<Address>().ToTable("Address");
            modelBuilder.Entity<UserChat>().ToTable("UserChat");

            //Required properties for User
            modelBuilder.Entity<User>()
                .Property(u => u.Username)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Password)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired();
            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired();

            //Required properties for Group
            modelBuilder.Entity<Group>()
                .Property(g => g.Name)
                .IsRequired();

            //Required properties for Message
            modelBuilder.Entity<Message>()
                .Property(m => m.Content)
                .IsRequired();

            //Required properties for Activity
            modelBuilder.Entity<Activity>()
                .Property(a => a.Description)
                .IsRequired();

            //Ignored User properties
            modelBuilder.Entity<User>()
                .Ignore(u => u.Friends);
            modelBuilder.Entity<User>()
                .Ignore(u => u.Age);

            //Many to one Group -> User (group admin)
            modelBuilder.Entity<User>()
                .HasMany(u => u.GroupsAdministrated)
                .WithOne(g => g.Admin)
                .HasForeignKey(g => g.AdminID);

            //Many to many User -> Group (via UserGroup)
            modelBuilder.Entity<UserGroup>()
                .HasKey(ug => new { ug.UserID, ug.GroupID });

            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupID);

            //Many to many User -> User (via Relationship)
            modelBuilder.Entity<Friendship>()
                .HasKey(f => new { f.UserID1, f.UserID2 });

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User1)
                .WithMany(u => u.FriendRequestsAccepted)
                .HasForeignKey(f => f.UserID1);

            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.User2)
                .WithMany(u => u.FriendRequestsMade)
                .HasForeignKey(f => f.UserID2)
                .OnDelete(DeleteBehavior.Restrict);

            // One to many Group -> Activity
            modelBuilder.Entity<Activity>()
                .HasOne(a => a.Group)
                .WithMany(g => g.Activities)
                .HasForeignKey(a => a.GroupID);

            // Many to many Plan -> User
            modelBuilder.Entity<PlanUser>()
                .HasKey(pu => new { pu.PlanID, pu.UserID });

            modelBuilder.Entity<PlanUser>()
                .HasOne(pu => pu.Plan)
                .WithMany(p => p.PlanUsers)
                .HasForeignKey(pu => pu.PlanID);

            modelBuilder.Entity<PlanUser>()
                .HasOne(pu => pu.User)
                .WithMany(p => p.PlanUsers)
                .HasForeignKey(pu => pu.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            // One to one Plan -> Chat
            modelBuilder.Entity<Plan>()
                .HasOne(p => p.Chat)
                .WithOne(c => c.Plan)
                .HasForeignKey<Plan>(p => p.ChatID);

            // One to many Message -> User
            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatID);

            // One to many Message -> Chat
            modelBuilder.Entity<Message>()
                .HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserID);

            // One to Many Group -> Plan
            modelBuilder.Entity<Plan>()
                .HasOne(p => p.Group)
                .WithMany(g => g.Plans)
                .HasForeignKey(p => p.GroupID);

            // One to Many User -> Address 
            modelBuilder.Entity<User>()
                .HasOne(u => u.Address)
                .WithMany(a => a.Users)
                .HasForeignKey(u => u.AddressID);

            // One to Many Plan -> Address
            modelBuilder.Entity<Plan>()
                .HasOne(p => p.Address)
                .WithMany(a => a.Plans)
                .HasForeignKey(p => p.AddressID)
                .OnDelete(DeleteBehavior.Restrict);

            // One to Many Plan -> Activities 
            modelBuilder.Entity<Plan>()
                .HasOne(p => p.Activity)
                .WithMany(a => a.Plans)
                .HasForeignKey(p => p.ActivityID)
                .OnDelete(DeleteBehavior.Restrict);

            // One to One Friendship -> Chat
            modelBuilder.Entity<Friendship>()
                .HasOne(f => f.Chat)
                .WithOne(c => c.Friendship)
                .HasForeignKey<Friendship>(f => f.ChatID);

            // Many to Many User -> Chat
            modelBuilder.Entity<UserChat>()
                .HasKey(uc => new { uc.ChatID, uc.UserID });

            modelBuilder.Entity<UserChat>()
                .HasOne(uc => uc.Chat)
                .WithMany(c => c.UserChats)
                .HasForeignKey(uc => uc.ChatID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserChat>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.UserChats)
                .HasForeignKey(uc => uc.UserID)
                .OnDelete(DeleteBehavior.Restrict);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);
        }
    }
}
