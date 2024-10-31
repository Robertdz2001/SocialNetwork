using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetworkBackend.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Posts2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Posts");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_CreatedUserId",
                table: "Posts",
                column: "CreatedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_CreatedUserId",
                table: "Posts",
                column: "CreatedUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_CreatedUserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_CreatedUserId",
                table: "Posts");

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Posts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
