using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeEmailIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_users_Email",
                schema: "dbo",
                table: "users");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Description", "Name", "NormalizedName" },
                values: new object[] { 3, "Guest role for the user", "Guest", "GUEST" });

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                schema: "dbo",
                table: "users",
                column: "Email",
                unique: true);
        }
    }
}
