using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blog.Migrations.BlockPost
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockPosts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CommentsId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreatorId = table.Column<string>(nullable: true),
                    LikesId = table.Column<string>(nullable: true),
                    PostsId = table.Column<string>(nullable: true),
                    RepostsId = table.Column<string>(nullable: true),
                    TagsId = table.Column<string>(nullable: true),
                    Theme = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockPosts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockPosts");
        }
    }
}
