using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class addTokenRefreshToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshTokens",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "55cfdd7a-f033-4f40-ba2a-960704af31d8", "AQAAAAIAAYagAAAAEIOKW4NBfuEv82TUml8DddSgqSbd22iQ83AMBeHw2Zswb1azraC43O/eMBufsQOo0w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "222595ac-6fd2-4aee-bb89-7a88acb1ddf4", "AQAAAAIAAYagAAAAEIu7VFi2umxSm6VpJIStZILyqZYC6JBAaHxmCWkopJeVwwOQNnM7RobWXlm5afoSQw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "35b6e586-b27b-42fc-b784-d0c2544a2005", "AQAAAAIAAYagAAAAEDcAGo5BjVvO+k/1/5Fm1treA/j4LfrDTc708o1mqKhKUgkQ+OVYZFaG2QLxqA0PSw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5b03f262-afbf-4d6f-94f6-6d7c877732b9", "AQAAAAIAAYagAAAAEARpBDB/lKZmhi+2qJQJ1gtYbVHPIoHzfAQtZTdNwhtXPezW0MMeRKZI1EyuMMc5VQ==" });
        }
    }
}
