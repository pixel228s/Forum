using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ignoreproperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_dbo_dbo_PostId",
                schema: "post_comments",
                table: "dbo");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo_dbo_UserId",
                schema: "post_comments",
                table: "dbo");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo_dbo_UserId",
                schema: "user_bans",
                table: "dbo");

            migrationBuilder.DropForeignKey(
                name: "FK_dbo_dbo_UserId",
                schema: "user_posts",
                table: "dbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo",
                schema: "users",
                table: "dbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo",
                schema: "user_posts",
                table: "dbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo",
                schema: "user_bans",
                table: "dbo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dbo",
                schema: "post_comments",
                table: "dbo");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.RenameTable(
                name: "dbo",
                schema: "users",
                newName: "users",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "dbo",
                schema: "user_posts",
                newName: "user_posts",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "dbo",
                schema: "user_bans",
                newName: "user_bans",
                newSchema: "dbo");

            migrationBuilder.RenameTable(
                name: "dbo",
                schema: "post_comments",
                newName: "post_comments",
                newSchema: "dbo");

            migrationBuilder.RenameIndex(
                name: "IX_dbo_Email",
                schema: "dbo",
                table: "users",
                newName: "IX_users_Email");

            migrationBuilder.RenameIndex(
                name: "IX_dbo_UserId",
                schema: "dbo",
                table: "user_posts",
                newName: "IX_user_posts_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_dbo_UserId",
                schema: "dbo",
                table: "user_bans",
                newName: "IX_user_bans_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_dbo_UserId",
                schema: "dbo",
                table: "post_comments",
                newName: "IX_post_comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_dbo_PostId",
                schema: "dbo",
                table: "post_comments",
                newName: "IX_post_comments_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                schema: "dbo",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_posts",
                schema: "dbo",
                table: "user_posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_bans",
                schema: "dbo",
                table: "user_bans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_post_comments",
                schema: "dbo",
                table: "post_comments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_post_comments_user_posts_PostId",
                schema: "dbo",
                table: "post_comments",
                column: "PostId",
                principalSchema: "dbo",
                principalTable: "user_posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_user_bans_users_UserId",
                schema: "dbo",
                table: "user_bans",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_post_comments_user_posts_PostId",
                schema: "dbo",
                table: "post_comments");

            migrationBuilder.DropForeignKey(
                name: "FK_post_comments_users_UserId",
                schema: "dbo",
                table: "post_comments");

            migrationBuilder.DropForeignKey(
                name: "FK_user_bans_users_UserId",
                schema: "dbo",
                table: "user_bans");

            migrationBuilder.DropForeignKey(
                name: "FK_user_posts_users_UserId",
                schema: "dbo",
                table: "user_posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                schema: "dbo",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_posts",
                schema: "dbo",
                table: "user_posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_bans",
                schema: "dbo",
                table: "user_bans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_post_comments",
                schema: "dbo",
                table: "post_comments");

            migrationBuilder.EnsureSchema(
                name: "post_comments");

            migrationBuilder.EnsureSchema(
                name: "user_bans");

            migrationBuilder.EnsureSchema(
                name: "user_posts");

            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "dbo",
                newName: "dbo",
                newSchema: "users");

            migrationBuilder.RenameTable(
                name: "user_posts",
                schema: "dbo",
                newName: "dbo",
                newSchema: "user_posts");

            migrationBuilder.RenameTable(
                name: "user_bans",
                schema: "dbo",
                newName: "dbo",
                newSchema: "user_bans");

            migrationBuilder.RenameTable(
                name: "post_comments",
                schema: "dbo",
                newName: "dbo",
                newSchema: "post_comments");

            migrationBuilder.RenameIndex(
                name: "IX_users_Email",
                schema: "users",
                table: "dbo",
                newName: "IX_dbo_Email");

            migrationBuilder.RenameIndex(
                name: "IX_user_posts_UserId",
                schema: "user_posts",
                table: "dbo",
                newName: "IX_dbo_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_user_bans_UserId",
                schema: "user_bans",
                table: "dbo",
                newName: "IX_dbo_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_post_comments_UserId",
                schema: "post_comments",
                table: "dbo",
                newName: "IX_dbo_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_post_comments_PostId",
                schema: "post_comments",
                table: "dbo",
                newName: "IX_dbo_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo",
                schema: "users",
                table: "dbo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo",
                schema: "user_posts",
                table: "dbo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo",
                schema: "user_bans",
                table: "dbo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dbo",
                schema: "post_comments",
                table: "dbo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_dbo_dbo_PostId",
                schema: "post_comments",
                table: "dbo",
                column: "PostId",
                principalSchema: "user_posts",
                principalTable: "dbo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo_dbo_UserId",
                schema: "post_comments",
                table: "dbo",
                column: "UserId",
                principalSchema: "users",
                principalTable: "dbo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo_dbo_UserId",
                schema: "user_bans",
                table: "dbo",
                column: "UserId",
                principalSchema: "users",
                principalTable: "dbo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dbo_dbo_UserId",
                schema: "user_posts",
                table: "dbo",
                column: "UserId",
                principalSchema: "users",
                principalTable: "dbo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
