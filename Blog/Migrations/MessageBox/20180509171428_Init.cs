using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blog.Migrations.MessageBox
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessagesBoxLayer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagesBoxLayer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MessagesLayer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagesLayer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserIndentity",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    MessageBoxId = table.Column<int>(nullable: true),
                    MessageBoxesId = table.Column<int>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    Picture = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserIndentityId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserIndentity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserIndentity_MessagesBoxLayer_MessageBoxesId",
                        column: x => x.MessageBoxesId,
                        principalTable: "MessagesBoxLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserIndentity_UserIndentity_UserIndentityId",
                        column: x => x.UserIndentityId,
                        principalTable: "UserIndentity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    MessageLevel = table.Column<int>(nullable: false),
                    MessagesLayerId = table.Column<int>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Message_MessagesLayer_MessagesLayerId",
                        column: x => x.MessagesLayerId,
                        principalTable: "MessagesLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Message_UserIndentity_UserId",
                        column: x => x.UserId,
                        principalTable: "UserIndentity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageBoxs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(nullable: true),
                    CreatorId = table.Column<string>(nullable: true),
                    MessageBoxLevel = table.Column<int>(nullable: false),
                    MessagesBoxLayerId = table.Column<int>(nullable: true),
                    MessagesId = table.Column<int>(nullable: true),
                    TimeCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageBoxs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageBoxs_UserIndentity_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "UserIndentity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageBoxs_MessagesBoxLayer_MessagesBoxLayerId",
                        column: x => x.MessagesBoxLayerId,
                        principalTable: "MessagesBoxLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageBoxs_MessagesLayer_MessagesId",
                        column: x => x.MessagesId,
                        principalTable: "MessagesLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_MessagesLayerId",
                table: "Message",
                column: "MessagesLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserId",
                table: "Message",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBoxs_CreatorId",
                table: "MessageBoxs",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBoxs_MessagesBoxLayerId",
                table: "MessageBoxs",
                column: "MessagesBoxLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBoxs_MessagesId",
                table: "MessageBoxs",
                column: "MessagesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserIndentity_MessageBoxId",
                table: "UserIndentity",
                column: "MessageBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_UserIndentity_MessageBoxesId",
                table: "UserIndentity",
                column: "MessageBoxesId");

            migrationBuilder.CreateIndex(
                name: "IX_UserIndentity_UserIndentityId",
                table: "UserIndentity",
                column: "UserIndentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserIndentity_MessageBoxs_MessageBoxId",
                table: "UserIndentity",
                column: "MessageBoxId",
                principalTable: "MessageBoxs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageBoxs_MessagesLayer_MessagesId",
                table: "MessageBoxs");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageBoxs_UserIndentity_CreatorId",
                table: "MessageBoxs");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "MessagesLayer");

            migrationBuilder.DropTable(
                name: "UserIndentity");

            migrationBuilder.DropTable(
                name: "MessageBoxs");

            migrationBuilder.DropTable(
                name: "MessagesBoxLayer");
        }
    }
}
