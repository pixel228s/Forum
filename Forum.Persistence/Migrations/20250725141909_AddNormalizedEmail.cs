using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNormalizedEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                schema: "dbo",
                table: "users",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_users_Email",
                schema: "dbo",
                table: "users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "EmailIndex",
                schema: "dbo",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_Email",
                schema: "dbo",
                table: "users");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                schema: "dbo",
                table: "users");
        }
    }
}
