using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_comments_users_UserId",
                schema: "dbo",
                table: "post_comments");

            migrationBuilder.DropForeignKey(
                name: "FK_user_posts_users_UserId",
                schema: "dbo",
                table: "user_posts");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                schema: "dbo",
                table: "users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "user_posts",
                type: "nvarchar(300)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "dbo",
                table: "user_posts",
                type: "nvarchar(4000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_post_comments_users_UserId",
                schema: "dbo",
                table: "post_comments",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_user_posts_users_UserId",
                schema: "dbo",
                table: "user_posts",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_comments_users_UserId",
                schema: "dbo",
                table: "post_comments");

            migrationBuilder.DropForeignKey(
                name: "FK_user_posts_users_UserId",
                schema: "dbo",
                table: "user_posts");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                schema: "dbo",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                schema: "dbo",
                table: "user_posts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(300)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                schema: "dbo",
                table: "user_posts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4000)");

            migrationBuilder.AddForeignKey(
                name: "FK_post_comments_users_UserId",
                schema: "dbo",
                table: "post_comments",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_posts_users_UserId",
                schema: "dbo",
                table: "user_posts",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
