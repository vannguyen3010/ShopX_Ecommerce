using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class StatusTableContact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Contacts",
                type: "bit",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Contacts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "058b684c-bcad-488e-9382-0924f81859fc", "AQAAAAIAAYagAAAAENZFsDVOwwlVSxfmKuyCIfNxoehtQWoDiX6Md53ImvUsp+6DhwawJzbzdNPczX94Wg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "53788e47-42e9-4a06-a63a-763eb0c62ef5", "AQAAAAIAAYagAAAAELAUWfDT3T1XTZqz03P5/CMY1B5q1LteFFsYmAPkDp4Zfid8wJkDb1+xRP/ZTB/37A==" });
        }
    }
}
