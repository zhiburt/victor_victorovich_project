﻿// <auto-generated />
using Blog.Db.Controllers;
using Blog.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Blog.Migrations.MessageBox
{
    [DbContext(typeof(MessageBoxContext))]
    [Migration("20180509171428_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10010");

            modelBuilder.Entity("Blog.Models.DbModels.Message", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<int>("MessageLevel");

                    b.Property<int?>("MessagesLayerId");

                    b.Property<DateTime>("Time");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MessagesLayerId");

                    b.HasIndex("UserId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Blog.Models.DbModels.MessageBox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("CreatorId");

                    b.Property<int>("MessageBoxLevel");

                    b.Property<int?>("MessagesBoxLayerId");

                    b.Property<int?>("MessagesId");

                    b.Property<DateTime>("TimeCreated");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("MessagesBoxLayerId");

                    b.HasIndex("MessagesId");

                    b.ToTable("MessageBoxs");
                });

            modelBuilder.Entity("Blog.Models.DbModels.MessagesBoxLayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("MessagesBoxLayer");
                });

            modelBuilder.Entity("Blog.Models.DbModels.MessagesLayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("MessagesLayer");
                });

            modelBuilder.Entity("Blog.Models.DbModels.UserIndentity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<int?>("MessageBoxId");

                    b.Property<int?>("MessageBoxesId");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("Picture");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserIndentityId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("MessageBoxId");

                    b.HasIndex("MessageBoxesId");

                    b.HasIndex("UserIndentityId");

                    b.ToTable("UserIndentity");
                });

            modelBuilder.Entity("Blog.Models.DbModels.Message", b =>
                {
                    b.HasOne("Blog.Models.DbModels.MessagesLayer")
                        .WithMany("Messages")
                        .HasForeignKey("MessagesLayerId");

                    b.HasOne("Blog.Models.DbModels.UserIndentity", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Blog.Models.DbModels.MessageBox", b =>
                {
                    b.HasOne("Blog.Models.DbModels.UserIndentity", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId");

                    b.HasOne("Blog.Models.DbModels.MessagesBoxLayer")
                        .WithMany("MessageBoxes")
                        .HasForeignKey("MessagesBoxLayerId");

                    b.HasOne("Blog.Models.DbModels.MessagesLayer", "Messages")
                        .WithMany()
                        .HasForeignKey("MessagesId");
                });

            modelBuilder.Entity("Blog.Models.DbModels.UserIndentity", b =>
                {
                    b.HasOne("Blog.Models.DbModels.MessageBox")
                        .WithMany("OtherUsers")
                        .HasForeignKey("MessageBoxId");

                    b.HasOne("Blog.Models.DbModels.MessagesBoxLayer", "MessageBoxes")
                        .WithMany()
                        .HasForeignKey("MessageBoxesId");

                    b.HasOne("Blog.Models.DbModels.UserIndentity")
                        .WithMany("Folowers")
                        .HasForeignKey("UserIndentityId");
                });
#pragma warning restore 612, 618
        }
    }
}
