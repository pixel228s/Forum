using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PostFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "picUrl",
                schema: "dbo",
                table: "user_posts",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                schema: "dbo",
                table: "user_posts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                schema: "dbo",
                table: "user_posts",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                schema: "dbo",
                table: "user_posts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                schema: "dbo",
                table: "user_posts");

            migrationBuilder.RenameColumn(
                name: "Title",
                schema: "dbo",
                table: "user_posts",
                newName: "picUrl");
        }
    }
}
