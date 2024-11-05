using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class StatusaddTableBannerBannerProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Banners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "BannerProducts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bacb6702-45f1-45c7-beaf-c59ff1f6ec63", "AQAAAAIAAYagAAAAEDpUgkyEmmAWrh9yetXP1lcJTr1FFmU5hkMFo1LX+rE7Rt7XSeykhtfvbVIKuDuOiw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "00e84c7e-70d0-474d-9e2f-1e51a90a44f3", "AQAAAAIAAYagAAAAEFcetdygx+OT6ewKv2Y12L+d8bQoYtdbFygA80P9U5IjtSgxbmI6Bj5s00rOZfk9ig==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BannerProducts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "139ea097-a43c-45e2-b109-a2f91b9417ed", "AQAAAAIAAYagAAAAENJBvUPs+4DUnxomxWL8xHtvB+exULg2vXHppj+F8JPrs9AyDC7RVaSx49pVvlYdQg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "6b8f7434-2c92-4354-b797-be17e981745c", "AQAAAAIAAYagAAAAEMmPxE/Juoqhdvq3ssBmxjRyDZWVxIpcuXHmS4yTUn/4/BCupRRkqhbhxrlZ9Knlqg==" });
        }
    }
}
