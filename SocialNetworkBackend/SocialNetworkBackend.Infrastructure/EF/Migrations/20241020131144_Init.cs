using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialNetworkBackend.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VerificationTokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    TokenHash = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<long>(type: "bigint", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    PhotoId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFriend",
                columns: table => new
                {
                    FriendId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFriend", x => new { x.FriendId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserFriend_Users_FriendId",
                        column: x => x.FriendId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFriend_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserInvite",
                columns: table => new
                {
                    ReceiverId = table.Column<long>(type: "bigint", nullable: false),
                    SenderId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInvite", x => new { x.ReceiverId, x.SenderId });
                    table.ForeignKey(
                        name: "FK_UserInvite_Users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserInvite_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "User" },
                    { 2L, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "City", "Country", "Email", "FirstName", "LastName", "PasswordHash", "PhoneNumber", "PhotoId", "RoleId" },
                values: new object[,]
                {
                    { 1L, "City1", "Country1", "user1@gmail.com", "FirstName1", "LastName1", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+1543654753", null, 1L },
                    { 2L, "City2", "Country2", "user2@gmail.com", "FirstName2", "LastName2", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+2543654753", null, 1L },
                    { 3L, "City3", "Country3", "user3@gmail.com", "FirstName3", "LastName3", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+3543654753", null, 1L },
                    { 4L, "City4", "Country4", "user4@gmail.com", "FirstName4", "LastName4", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+4543654753", null, 1L },
                    { 5L, "City5", "Country5", "user5@gmail.com", "FirstName5", "LastName5", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+5543654753", null, 1L },
                    { 6L, "City6", "Country6", "user6@gmail.com", "FirstName6", "LastName6", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+6543654753", null, 1L },
                    { 7L, "City7", "Country7", "user7@gmail.com", "FirstName7", "LastName7", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+7543654753", null, 1L },
                    { 8L, "City8", "Country8", "user8@gmail.com", "FirstName8", "LastName8", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+8543654753", null, 1L },
                    { 9L, "City9", "Country9", "user9@gmail.com", "FirstName9", "LastName9", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+9543654753", null, 1L },
                    { 10L, "City10", "Country10", "user10@gmail.com", "FirstName10", "LastName10", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+10543654753", null, 1L },
                    { 11L, "City11", "Country11", "user11@gmail.com", "FirstName11", "LastName11", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+11543654753", null, 1L },
                    { 12L, "City12", "Country12", "user12@gmail.com", "FirstName12", "LastName12", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+12543654753", null, 1L },
                    { 13L, "City13", "Country13", "user13@gmail.com", "FirstName13", "LastName13", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+13543654753", null, 1L },
                    { 14L, "City14", "Country14", "user14@gmail.com", "FirstName14", "LastName14", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+14543654753", null, 1L },
                    { 15L, "City15", "Country15", "user15@gmail.com", "FirstName15", "LastName15", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+15543654753", null, 1L },
                    { 16L, "City16", "Country16", "user16@gmail.com", "FirstName16", "LastName16", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+16543654753", null, 1L },
                    { 17L, "City17", "Country17", "user17@gmail.com", "FirstName17", "LastName17", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+17543654753", null, 1L },
                    { 18L, "City18", "Country18", "user18@gmail.com", "FirstName18", "LastName18", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+18543654753", null, 1L },
                    { 19L, "City19", "Country19", "user19@gmail.com", "FirstName19", "LastName19", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+19543654753", null, 1L },
                    { 20L, "City20", "Country20", "user20@gmail.com", "FirstName20", "LastName20", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+20543654753", null, 1L },
                    { 21L, "City21", "Country21", "user21@gmail.com", "FirstName21", "LastName21", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+21543654753", null, 1L },
                    { 22L, "City22", "Country22", "user22@gmail.com", "FirstName22", "LastName22", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+22543654753", null, 1L },
                    { 23L, "City23", "Country23", "user23@gmail.com", "FirstName23", "LastName23", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+23543654753", null, 1L },
                    { 24L, "City24", "Country24", "user24@gmail.com", "FirstName24", "LastName24", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+24543654753", null, 1L },
                    { 25L, "City25", "Country25", "user25@gmail.com", "FirstName25", "LastName25", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+25543654753", null, 1L },
                    { 26L, "City26", "Country26", "user26@gmail.com", "FirstName26", "LastName26", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+26543654753", null, 1L },
                    { 27L, "City27", "Country27", "user27@gmail.com", "FirstName27", "LastName27", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+27543654753", null, 1L },
                    { 28L, "City28", "Country28", "user28@gmail.com", "FirstName28", "LastName28", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+28543654753", null, 1L },
                    { 29L, "City29", "Country29", "user29@gmail.com", "FirstName29", "LastName29", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+29543654753", null, 1L },
                    { 30L, "City30", "Country30", "user30@gmail.com", "FirstName30", "LastName30", "AQAAAAIAAYagAAAAEPsuyOqmTpouyfPJVn/fMBcJyimk/UZqAvH8tgBJG+xaDr3yMmCM0pxy3ex8taUY9A==", "+30543654753", null, 1L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photos_UserId",
                table: "Photos",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserFriend_UserId",
                table: "UserFriend",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserInvite_SenderId",
                table: "UserInvite",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "UserFriend");

            migrationBuilder.DropTable(
                name: "UserInvite");

            migrationBuilder.DropTable(
                name: "VerificationTokens");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
