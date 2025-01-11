using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Job.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_applicant_status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Sort = table.Column<int>(type: "integer", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_applicant_status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_experiences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_experiences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_genders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_populars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Background = table.Column<string>(type: "text", nullable: false),
                    TargetApplicants = table.Column<int>(type: "integer", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_populars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_provinces",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Area = table.Column<string>(type: "text", nullable: false),
                    AreaName = table.Column<string>(type: "text", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_ranks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ranks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_rep_companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Fullname = table.Column<string>(type: "text", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_rep_companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_work_types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_work_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    SalaryType = table.Column<int>(type: "integer", nullable: false),
                    SalaryMin = table.Column<decimal>(type: "numeric", nullable: false),
                    SalaryMax = table.Column<decimal>(type: "numeric", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Available = table.Column<bool>(type: "boolean", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Requirement = table.Column<string>(type: "text", nullable: false),
                    Benefit = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    WorkingTime = table.Column<string>(type: "text", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    RankId = table.Column<Guid>(type: "uuid", nullable: true),
                    GenderId = table.Column<Guid>(type: "uuid", nullable: true),
                    ExperienceId = table.Column<Guid>(type: "uuid", nullable: true),
                    WorkTypeId = table.Column<Guid>(type: "uuid", nullable: true),
                    PopularId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_jobs_tb_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "tb_categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_jobs_tb_experiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "tb_experiences",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_jobs_tb_genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "tb_genders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_jobs_tb_populars_PopularId",
                        column: x => x.PopularId,
                        principalTable: "tb_populars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_jobs_tb_ranks_RankId",
                        column: x => x.RankId,
                        principalTable: "tb_ranks",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_jobs_tb_rep_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tb_rep_companies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_jobs_tb_work_types_WorkTypeId",
                        column: x => x.WorkTypeId,
                        principalTable: "tb_work_types",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_applicants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Fullname = table.Column<string>(type: "text", nullable: false),
                    Profile = table.Column<string>(type: "text", nullable: false),
                    CoverLetter = table.Column<string>(type: "text", nullable: false),
                    JobId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    StatusId = table.Column<int>(type: "integer", nullable: true),
                    DeleteFlag = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedUser = table.Column<Guid>(type: "uuid", nullable: true),
                    ModifiedUser = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_applicants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_applicants_tb_applicant_status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "tb_applicant_status",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tb_applicants_tb_jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "tb_jobs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "tb_work_locations",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProvinceId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_work_locations", x => new { x.JobId, x.ProvinceId });
                    table.ForeignKey(
                        name: "FK_tb_work_locations_tb_jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "tb_jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_work_locations_tb_provinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "tb_provinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_applicants_JobId",
                table: "tb_applicants",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_applicants_StatusId",
                table: "tb_applicants",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_jobs_CategoryId",
                table: "tb_jobs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_jobs_CompanyId",
                table: "tb_jobs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_jobs_ExperienceId",
                table: "tb_jobs",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_jobs_GenderId",
                table: "tb_jobs",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_jobs_PopularId",
                table: "tb_jobs",
                column: "PopularId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_jobs_RankId",
                table: "tb_jobs",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_jobs_WorkTypeId",
                table: "tb_jobs",
                column: "WorkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_work_locations_ProvinceId",
                table: "tb_work_locations",
                column: "ProvinceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_applicants");

            migrationBuilder.DropTable(
                name: "tb_work_locations");

            migrationBuilder.DropTable(
                name: "tb_applicant_status");

            migrationBuilder.DropTable(
                name: "tb_jobs");

            migrationBuilder.DropTable(
                name: "tb_provinces");

            migrationBuilder.DropTable(
                name: "tb_categories");

            migrationBuilder.DropTable(
                name: "tb_experiences");

            migrationBuilder.DropTable(
                name: "tb_genders");

            migrationBuilder.DropTable(
                name: "tb_populars");

            migrationBuilder.DropTable(
                name: "tb_ranks");

            migrationBuilder.DropTable(
                name: "tb_rep_companies");

            migrationBuilder.DropTable(
                name: "tb_work_types");
        }
    }
}
