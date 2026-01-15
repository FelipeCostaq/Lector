using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LectorASPNET.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    auth_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    bio = table.Column<string>(type: "text", nullable: true),
                    avatar_url = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    book_id = table.Column<string>(type: "text", nullable: false),
                    book_title = table.Column<string>(type: "text", nullable: false),
                    book_cover_url = table.Column<string>(type: "text", nullable: true),
                    rating = table.Column<double>(type: "double precision", nullable: false),
                    review_text = table.Column<string>(type: "text", nullable: true),
                    publication_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "socials",
                columns: table => new
                {
                    follower_id = table.Column<Guid>(type: "uuid", nullable: false),
                    following_id = table.Column<Guid>(type: "uuid", nullable: false),
                    follow_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_socials", x => new { x.follower_id, x.following_id });
                    table.ForeignKey(
                        name: "fk_socials_users_follower_id",
                        column: x => x.follower_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_socials_users_following_id",
                        column: x => x.following_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user_libraries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    book_id = table.Column<string>(type: "text", nullable: false),
                    book_title = table.Column<string>(type: "text", nullable: false),
                    book_cover_url = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "text", nullable: false),
                    update_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_libraries", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_libraries_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_reviews_book_id",
                table: "reviews",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_user_id",
                table: "reviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_socials_following_id",
                table: "socials",
                column: "following_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_libraries_book_id",
                table: "user_libraries",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_libraries_user_id",
                table: "user_libraries",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_auth_id",
                table: "users",
                column: "auth_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "socials");

            migrationBuilder.DropTable(
                name: "user_libraries");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
