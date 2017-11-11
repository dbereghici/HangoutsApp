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
    [Migration("20171109182653_GroupActivityMigration")]
    partial class GroupActivityMigration
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

                    b.HasKey("ID");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupAdministratedID");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.HasIndex("GroupAdministratedID")
                        .IsUnique();

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
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("HangoutsDbLibrary.Model.User", "User2")
                        .WithMany("Friends1")
                        .HasForeignKey("UserID2")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("HangoutsDbLibrary.Model.User", b =>
                {
                    b.HasOne("HangoutsDbLibrary.Model.Group", "GroupAdministrated")
                        .WithOne("Admin")
                        .HasForeignKey("HangoutsDbLibrary.Model.User", "GroupAdministratedID")
                        .OnDelete(DeleteBehavior.Cascade);
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
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
