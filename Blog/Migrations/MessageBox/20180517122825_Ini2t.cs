using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blog.Migrations.MessageBox
{
    public partial class Ini2t : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessageBoxs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(nullable: true),
                    CreatorId = table.Column<string>(nullable: true),
                    MessageBoxLevel = table.Column<int>(nullable: false),
                    MessagesId = table.Column<string>(nullable: true),
                    OtherUsersId = table.Column<string>(nullable: true),
                    TimeCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageBoxs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessageBoxs");
        }
    }
}
