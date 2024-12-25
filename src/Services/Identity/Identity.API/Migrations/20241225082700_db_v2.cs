using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.API.Migrations
{
    /// <inheritdoc />
    public partial class db_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompanyInfos_Sizes_SizeId",
                table: "CompanyInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyInfos_tb_users_Id",
                table: "CompanyInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_CoverLetters_tb_users_UserId",
                table: "CoverLetters");

            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_tb_users_UserId",
                table: "Profiles");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_company_province_CompanyInfos_CompanyId",
                table: "tb_company_province");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_company_province_Provinces_ProvinceId",
                table: "tb_company_province");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sizes",
                table: "Sizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoverLetters",
                table: "CoverLetters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyInfos",
                table: "CompanyInfos");

            migrationBuilder.RenameTable(
                name: "Sizes",
                newName: "tb_sizes");

            migrationBuilder.RenameTable(
                name: "Provinces",
                newName: "tb_provinces");

            migrationBuilder.RenameTable(
                name: "Profiles",
                newName: "tb_profiles");

            migrationBuilder.RenameTable(
                name: "CoverLetters",
                newName: "tb_cover_letters");

            migrationBuilder.RenameTable(
                name: "CompanyInfos",
                newName: "tb_companies");

            migrationBuilder.RenameIndex(
                name: "IX_Profiles_UserId",
                table: "tb_profiles",
                newName: "IX_tb_profiles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CoverLetters_UserId",
                table: "tb_cover_letters",
                newName: "IX_tb_cover_letters_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyInfos_SizeId",
                table: "tb_companies",
                newName: "IX_tb_companies_SizeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_sizes",
                table: "tb_sizes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_provinces",
                table: "tb_provinces",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_profiles",
                table: "tb_profiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_cover_letters",
                table: "tb_cover_letters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_companies",
                table: "tb_companies",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_companies_tb_sizes_SizeId",
                table: "tb_companies",
                column: "SizeId",
                principalTable: "tb_sizes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_companies_tb_users_Id",
                table: "tb_companies",
                column: "Id",
                principalTable: "tb_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_company_province_tb_companies_CompanyId",
                table: "tb_company_province",
                column: "CompanyId",
                principalTable: "tb_companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_company_province_tb_provinces_ProvinceId",
                table: "tb_company_province",
                column: "ProvinceId",
                principalTable: "tb_provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_cover_letters_tb_users_UserId",
                table: "tb_cover_letters",
                column: "UserId",
                principalTable: "tb_users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_profiles_tb_users_UserId",
                table: "tb_profiles",
                column: "UserId",
                principalTable: "tb_users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_companies_tb_sizes_SizeId",
                table: "tb_companies");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_companies_tb_users_Id",
                table: "tb_companies");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_company_province_tb_companies_CompanyId",
                table: "tb_company_province");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_company_province_tb_provinces_ProvinceId",
                table: "tb_company_province");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_cover_letters_tb_users_UserId",
                table: "tb_cover_letters");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_profiles_tb_users_UserId",
                table: "tb_profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_sizes",
                table: "tb_sizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_provinces",
                table: "tb_provinces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_profiles",
                table: "tb_profiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_cover_letters",
                table: "tb_cover_letters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_companies",
                table: "tb_companies");

            migrationBuilder.RenameTable(
                name: "tb_sizes",
                newName: "Sizes");

            migrationBuilder.RenameTable(
                name: "tb_provinces",
                newName: "Provinces");

            migrationBuilder.RenameTable(
                name: "tb_profiles",
                newName: "Profiles");

            migrationBuilder.RenameTable(
                name: "tb_cover_letters",
                newName: "CoverLetters");

            migrationBuilder.RenameTable(
                name: "tb_companies",
                newName: "CompanyInfos");

            migrationBuilder.RenameIndex(
                name: "IX_tb_profiles_UserId",
                table: "Profiles",
                newName: "IX_Profiles_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_cover_letters_UserId",
                table: "CoverLetters",
                newName: "IX_CoverLetters_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_tb_companies_SizeId",
                table: "CompanyInfos",
                newName: "IX_CompanyInfos_SizeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sizes",
                table: "Sizes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Provinces",
                table: "Provinces",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Profiles",
                table: "Profiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoverLetters",
                table: "CoverLetters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyInfos",
                table: "CompanyInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyInfos_Sizes_SizeId",
                table: "CompanyInfos",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyInfos_tb_users_Id",
                table: "CompanyInfos",
                column: "Id",
                principalTable: "tb_users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoverLetters_tb_users_UserId",
                table: "CoverLetters",
                column: "UserId",
                principalTable: "tb_users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_tb_users_UserId",
                table: "Profiles",
                column: "UserId",
                principalTable: "tb_users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_company_province_CompanyInfos_CompanyId",
                table: "tb_company_province",
                column: "CompanyId",
                principalTable: "CompanyInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_company_province_Provinces_ProvinceId",
                table: "tb_company_province",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
