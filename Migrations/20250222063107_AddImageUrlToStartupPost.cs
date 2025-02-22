using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StartupPitchingPlatform.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToStartupPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "StartupPosts",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "StartupPosts",
                newName: "ImagePath");
        }
    }
}
