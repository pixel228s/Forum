using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                schema: "dbo",
                table: "user_posts",
                type: "int",
                nullable: false,
                defaultValue: 2);

            migrationBuilder.CreateIndex(
                name: "IX_users_UserName",
                schema: "dbo",
                table: "users",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_bans_BanEndDate",
                schema: "dbo",
                table: "user_bans",
                column: "BanEndDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_UserName",
                schema: "dbo",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_user_bans_BanEndDate",
                schema: "dbo",
                table: "user_bans");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "dbo",
                table: "user_posts");
        }
    }
}
