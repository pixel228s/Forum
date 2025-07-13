using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "post_comments");

            migrationBuilder.EnsureSchema(
                name: "user_bans");

            migrationBuilder.EnsureSchema(
                name: "user_posts");

            migrationBuilder.EnsureSchema(
                name: "users");

            migrationBuilder.CreateTable(
                name: "dbo",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    picUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsBanned = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserName = table.Column<string>(type: "varchar(100)", nullable: false),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "varchar(120)", fixedLength: true, maxLength: 120, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "dbo",
                schema: "user_bans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BanReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BanEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo_dbo_UserId",
                        column: x => x.UserId,
                        principalSchema: "users",
                        principalTable: "dbo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "dbo",
                schema: "user_posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    State = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo_dbo_UserId",
                        column: x => x.UserId,
                        principalSchema: "users",
                        principalTable: "dbo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "dbo",
                schema: "post_comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PostId = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(3000)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dbo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dbo_dbo_PostId",
                        column: x => x.PostId,
                        principalSchema: "user_posts",
                        principalTable: "dbo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_dbo_dbo_UserId",
                        column: x => x.UserId,
                        principalSchema: "users",
                        principalTable: "dbo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_dbo_PostId",
                schema: "post_comments",
                table: "dbo",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_dbo_UserId",
                schema: "post_comments",
                table: "dbo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_dbo_UserId",
                schema: "user_bans",
                table: "dbo",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_dbo_UserId",
                schema: "user_posts",
                table: "dbo",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_dbo_Email",
                schema: "users",
                table: "dbo",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dbo",
                schema: "post_comments");

            migrationBuilder.DropTable(
                name: "dbo",
                schema: "user_bans");

            migrationBuilder.DropTable(
                name: "dbo",
                schema: "user_posts");

            migrationBuilder.DropTable(
                name: "dbo",
                schema: "users");
        }
    }
}
