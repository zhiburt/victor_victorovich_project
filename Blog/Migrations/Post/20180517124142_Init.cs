using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blog.Migrations.Post
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CommentsId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreatorId = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    LikesId = table.Column<string>(nullable: true),
                    RepostsId = table.Column<string>(nullable: true),
                    TagsId = table.Column<string>(nullable: true),
                    Theme = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
