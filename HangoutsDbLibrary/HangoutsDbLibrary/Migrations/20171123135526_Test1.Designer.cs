﻿// <auto-generated />
using HangoutsDbLibrary.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace HangoutsDbLibrary.Migrations
{
    [DbContext(typeof(HangoutsContext))]
    [Migration("20171123135526_Test1")]
    partial class Test1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("HangoutsDbLibrary.Model.Activity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupID");

                    b.HasKey("ID");

                    b.HasIndex("GroupID");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Chat", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ID");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Friendship", b =>
                {
                    b.Property<int>("UserID1");

                    b.Property<int>("UserID2");

                    b.HasKey("UserID1", "UserID2");

                    b.HasIndex("UserID2");

                    b.ToTable("Friendship");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Group", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AdminID");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("AdminID");

                    b.HasIndex("UserID");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Message", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChatID");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("UserID");

                    b.HasKey("ID");

                    b.HasIndex("ChatID");

                    b.HasIndex("UserID");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Plan", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ChatID");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("GroupID");

                    b.Property<int>("LifeTime");

                    b.HasKey("ID");

                    b.HasIndex("ChatID")
                        .IsUnique();

                    b.HasIndex("GroupID");

                    b.ToTable("Plan");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.PlanUser", b =>
                {
                    b.Property<int>("PlanID");

                    b.Property<int>("UserID");

                    b.HasKey("PlanID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("PlanUser");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Location");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(25);

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.UserGroup", b =>
                {
                    b.Property<int>("UserID");

                    b.Property<int>("GroupID");

                    b.HasKey("UserID", "GroupID");

                    b.HasIndex("GroupID");

                    b.ToTable("UserGroup");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Activity", b =>
                {
                    b.HasOne("HangoutsDbLibrary.Model.Group", "Group")
                        .WithMany("Activities")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Friendship", b =>
                {
                    b.HasOne("HangoutsDbLibrary.Model.User", "User1")
                        .WithMany("Friends2")
                        .HasForeignKey("UserID1")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HangoutsDbLibrary.Model.User", "User2")
                        .WithMany("Friends1")
                        .HasForeignKey("UserID2")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Group", b =>
                {
                    b.HasOne("HangoutsDbLibrary.Model.User", "Admin")
                        .WithMany("GroupsAdministrated")
                        .HasForeignKey("AdminID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HangoutsDbLibrary.Model.User")
                        .WithMany("Groups")
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Message", b =>
                {
                    b.HasOne("HangoutsDbLibrary.Model.Chat", "Chat")
                        .WithMany("Messages")
                        .HasForeignKey("ChatID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HangoutsDbLibrary.Model.User", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.Plan", b =>
                {
                    b.HasOne("HangoutsDbLibrary.Model.Chat", "Chat")
                        .WithOne("Plan")
                        .HasForeignKey("HangoutsDbLibrary.Model.Plan", "ChatID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HangoutsDbLibrary.Model.Group", "Group")
                        .WithMany("Plans")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.PlanUser", b =>
                {
                    b.HasOne("HangoutsDbLibrary.Model.Plan", "Plan")
                        .WithMany("PlanUsers")
                        .HasForeignKey("PlanID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HangoutsDbLibrary.Model.User", "User")
                        .WithMany("PlanUsers")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.UserGroup", b =>
                {
                    b.HasOne("HangoutsDbLibrary.Model.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HangoutsDbLibrary.Model.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
