using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Blog.Migrations.Repost
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentsLayer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsLayer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LikeLayer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeLayer", x => x.Id);
                });

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
                name: "RepostsLayer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepostsLayer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagsLayer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsLayer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CommentId = table.Column<string>(nullable: true),
                    LikeLayerId = table.Column<int>(nullable: true),
                    LikeLevel = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Like_LikeLayer_LikeLayerId",
                        column: x => x.LikeLayerId,
                        principalTable: "LikeLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reposts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CommentId = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    LikesId = table.Column<int>(nullable: true),
                    NaturePostId = table.Column<string>(nullable: true),
                    RepostsLayerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reposts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reposts_LikeLayer_LikesId",
                        column: x => x.LikesId,
                        principalTable: "LikeLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reposts_RepostsLayer_RepostsLayerId",
                        column: x => x.RepostsLayerId,
                        principalTable: "RepostsLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AbsentUserId = table.Column<string>(nullable: true),
                    CommentsLayerId = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comment_CommentsLayer_CommentsLayerId",
                        column: x => x.CommentsLayerId,
                        principalTable: "CommentsLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CommentsId = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreatorId = table.Column<string>(nullable: true),
                    LikesId = table.Column<int>(nullable: true),
                    RepostsId = table.Column<int>(nullable: true),
                    TagsId = table.Column<int>(nullable: true),
                    Theme = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Post_CommentsLayer_CommentsId",
                        column: x => x.CommentsId,
                        principalTable: "CommentsLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_LikeLayer_LikesId",
                        column: x => x.LikesId,
                        principalTable: "LikeLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_RepostsLayer_RepostsId",
                        column: x => x.RepostsId,
                        principalTable: "RepostsLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Post_TagsLayer_TagsId",
                        column: x => x.TagsId,
                        principalTable: "TagsLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "MessageBox",
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
                    table.PrimaryKey("PK_MessageBox", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageBox_UserIndentity_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "UserIndentity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageBox_MessagesBoxLayer_MessagesBoxLayerId",
                        column: x => x.MessagesBoxLayerId,
                        principalTable: "MessagesBoxLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MessageBox_MessagesLayer_MessagesId",
                        column: x => x.MessagesId,
                        principalTable: "MessagesLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Fame = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TagsLayerId = table.Column<int>(nullable: true),
                    UserCreatedId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tag_TagsLayer_TagsLayerId",
                        column: x => x.TagsLayerId,
                        principalTable: "TagsLayer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tag_UserIndentity_UserCreatedId",
                        column: x => x.UserCreatedId,
                        principalTable: "UserIndentity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AbsentUserId",
                table: "Comment",
                column: "AbsentUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_CommentsLayerId",
                table: "Comment",
                column: "CommentsLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_UserId",
                table: "Comment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_CommentId",
                table: "Like",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_LikeLayerId",
                table: "Like",
                column: "LikeLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_UserId",
                table: "Like",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_MessagesLayerId",
                table: "Message",
                column: "MessagesLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_UserId",
                table: "Message",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBox_CreatorId",
                table: "MessageBox",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBox_MessagesBoxLayerId",
                table: "MessageBox",
                column: "MessagesBoxLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MessageBox_MessagesId",
                table: "MessageBox",
                column: "MessagesId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CommentsId",
                table: "Post",
                column: "CommentsId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_CreatorId",
                table: "Post",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_LikesId",
                table: "Post",
                column: "LikesId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_RepostsId",
                table: "Post",
                column: "RepostsId");

            migrationBuilder.CreateIndex(
                name: "IX_Post_TagsId",
                table: "Post",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Reposts_CommentId",
                table: "Reposts",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reposts_LikesId",
                table: "Reposts",
                column: "LikesId");

            migrationBuilder.CreateIndex(
                name: "IX_Reposts_NaturePostId",
                table: "Reposts",
                column: "NaturePostId");

            migrationBuilder.CreateIndex(
                name: "IX_Reposts_RepostsLayerId",
                table: "Reposts",
                column: "RepostsLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_TagsLayerId",
                table: "Tag",
                column: "TagsLayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_UserCreatedId",
                table: "Tag",
                column: "UserCreatedId");

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
                name: "FK_Like_UserIndentity_UserId",
                table: "Like",
                column: "UserId",
                principalTable: "UserIndentity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Like_Comment_CommentId",
                table: "Like",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reposts_Comment_CommentId",
                table: "Reposts",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reposts_Post_NaturePostId",
                table: "Reposts",
                column: "NaturePostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_UserIndentity_AbsentUserId",
                table: "Comment",
                column: "AbsentUserId",
                principalTable: "UserIndentity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_UserIndentity_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "UserIndentity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_UserIndentity_CreatorId",
                table: "Post",
                column: "CreatorId",
                principalTable: "UserIndentity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserIndentity_MessageBox_MessageBoxId",
                table: "UserIndentity",
                column: "MessageBoxId",
                principalTable: "MessageBox",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageBox_UserIndentity_CreatorId",
                table: "MessageBox");

            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Reposts");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropTable(
                name: "CommentsLayer");

            migrationBuilder.DropTable(
                name: "LikeLayer");

            migrationBuilder.DropTable(
                name: "RepostsLayer");

            migrationBuilder.DropTable(
                name: "TagsLayer");

            migrationBuilder.DropTable(
                name: "UserIndentity");

            migrationBuilder.DropTable(
                name: "MessageBox");

            migrationBuilder.DropTable(
                name: "MessagesBoxLayer");

            migrationBuilder.DropTable(
                name: "MessagesLayer");
        }
    }
}
