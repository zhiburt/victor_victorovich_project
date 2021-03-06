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
    partial class MessageBoxContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10010");

            modelBuilder.Entity("Blog.Models.DbModels.MessageBox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("CreatorId");

                    b.Property<int>("MessageBoxLevel");

                    b.Property<string>("MessagesId");

                    b.Property<string>("Name");

                    b.Property<string>("OtherUsersId");

                    b.Property<DateTime>("TimeCreated");

                    b.HasKey("Id");

                    b.ToTable("MessageBoxs");
                });
#pragma warning restore 612, 618
        }
    }
}
