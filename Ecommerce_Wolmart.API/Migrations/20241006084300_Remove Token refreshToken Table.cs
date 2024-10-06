using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTokenrefreshTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "RefreshTokens");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "a92b711d-e064-4650-b5e9-ae1aec1e7376", "AQAAAAIAAYagAAAAECLm3B/qGONJ+Smb5TNKb5nL3bTRo/lamJoOq8D8zMNVgCjWzy59MZacvssGlCeo/A==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b5823a9d-1920-4a02-9114-6b285bb5e7a2", "AQAAAAIAAYagAAAAEPkcfWZZXROEaMJjjO8A/v1BztOoUFj5ZO4FGt8w1MXQagLs7BpjAxH8It3/WdORvA==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Token",
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
    }
}
