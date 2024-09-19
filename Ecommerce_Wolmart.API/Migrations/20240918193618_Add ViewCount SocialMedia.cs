using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class AddViewCountSocialMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "SocialMediaInfos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d705105b-2437-4180-b782-a583cfc1a0a7", "AQAAAAIAAYagAAAAEB5D/Xz75Y8uDdMaKYOx1HF79WbcTckP4pKFEY6zFNaPtppZ4UJ0eUXwoco6V2NAsA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "36ed7b0e-5d59-4b56-9f23-6b650772066d", "AQAAAAIAAYagAAAAED7AtJ08kdTl+Fc7ri4VvM0Mtjw6pyEdt3VJWtdUWUFQLatdTKnhryrniQ9g0+nzIg==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "SocialMediaInfos");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "265bcddb-ac42-4eda-a814-27660cbaa596", "AQAAAAIAAYagAAAAEDspurEl0PJ4H636xri8SLo/LuzhAuqitet6O7MoTPqIsYZsOPeWaEPKsZgTQFP3TA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3642ed95-210a-4aca-80b3-1f7865cf1972", "AQAAAAIAAYagAAAAEBA7BGX+zPwA/7EExH2/S5v09nB+p4q8j0aiIUWF2hEfB4i4Hg0DBMsIDxfM85VAag==" });
        }
    }
}
