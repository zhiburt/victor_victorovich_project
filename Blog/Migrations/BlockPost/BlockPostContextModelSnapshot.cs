﻿// <auto-generated />
using Blog.Db.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Blog.Migrations.BlockPost
{
    [DbContext(typeof(BlockPostContext))]
    partial class BlockPostContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10010");

            modelBuilder.Entity("Blog.Models.DbModels.BlockPost", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CommentsId");

                    b.Property<string>("Content");

                    b.Property<string>("CreatorId");

                    b.Property<string>("LikesId");

                    b.Property<string>("PostsId");

                    b.Property<string>("RepostsId");

                    b.Property<string>("TagsId");

                    b.Property<string>("Theme");

                    b.HasKey("Id");

                    b.ToTable("BlockPosts");
                });
#pragma warning restore 612, 618
        }
    }
}
