using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StartupPitchingPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryAndSocialLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "StartupPosts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GitHubUrl",
                table: "StartupPosts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedInUrl",
                table: "StartupPosts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "StartupPosts");

            migrationBuilder.DropColumn(
                name: "GitHubUrl",
                table: "StartupPosts");

            migrationBuilder.DropColumn(
                name: "LinkedInUrl",
                table: "StartupPosts");
        }
    }
}
