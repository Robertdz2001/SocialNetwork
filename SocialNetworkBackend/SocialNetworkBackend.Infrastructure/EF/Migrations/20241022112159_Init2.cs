using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetworkBackend.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvites_Users_ReceiverId",
                table: "FriendInvites");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvites_Users_SenderId",
                table: "FriendInvites");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvites_Users_ReceiverId",
                table: "FriendInvites",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvites_Users_SenderId",
                table: "FriendInvites",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvites_Users_ReceiverId",
                table: "FriendInvites");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendInvites_Users_SenderId",
                table: "FriendInvites");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvites_Users_ReceiverId",
                table: "FriendInvites",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendInvites_Users_SenderId",
                table: "FriendInvites",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
