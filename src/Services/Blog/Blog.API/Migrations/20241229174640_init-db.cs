using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.API.Migrations
{
    /// <inheritdoc />
    public partial class initdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_tagNames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_tagNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Slug = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_posts_tb_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tb_categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_posts_tb_status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "tb_status",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_post_tagName",
                columns: table => new
                {
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagNameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_post_tagName", x => new { x.PostId, x.TagNameId });
                    table.ForeignKey(
                        name: "FK_tb_post_tagName_tb_posts_PostId",
                        column: x => x.PostId,
                        principalTable: "tb_posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_post_tagName_tb_tagNames_TagNameId",
                        column: x => x.TagNameId,
                        principalTable: "tb_tagNames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_postSaves",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_postSaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_postSaves_tb_posts_PostId",
                        column: x => x.PostId,
                        principalTable: "tb_posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_categories_Slug",
                table: "tb_categories",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_post_tagName_TagNameId",
                table: "tb_post_tagName",
                column: "TagNameId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_posts_CategoryId",
                table: "tb_posts",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_posts_Slug",
                table: "tb_posts",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_posts_StatusId",
                table: "tb_posts",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_posts_UserId",
                table: "tb_posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_postSaves_PostId",
                table: "tb_postSaves",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_postSaves_UserId",
                table: "tb_postSaves",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_status_Slug",
                table: "tb_status",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_tagNames_Slug",
                table: "tb_tagNames",
                column: "Slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_post_tagName");

            migrationBuilder.DropTable(
                name: "tb_postSaves");

            migrationBuilder.DropTable(
                name: "tb_tagNames");

            migrationBuilder.DropTable(
                name: "tb_posts");

            migrationBuilder.DropTable(
                name: "tb_categories");

            migrationBuilder.DropTable(
                name: "tb_status");
        }
    }
}
