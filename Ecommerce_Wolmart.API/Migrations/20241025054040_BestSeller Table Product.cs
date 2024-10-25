using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce_Wolmart.API.Migrations
{
    /// <inheritdoc />
    public partial class BestSellerTableProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BestSeller",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "0d0cc9f4-7855-4626-825b-8722846f2185", "AQAAAAIAAYagAAAAEIsyNV282gddTdUOtJl7SuGU6Qj2mjDfdTdiOm0E4q76clMHracaA5qNk6zcsH/oEA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f8a6f1c9-d2e0-4121-a236-ee15af4e3719", "AQAAAAIAAYagAAAAEGfjNhB5O+1h8E7ekA3oo7Tx1i8LZUuhzG/R6qhJu8HTijtv1ZVWMpZ61mCJphr72w==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestSeller",
                table: "Products");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2bd32c0-d75e-4966-8274-758e273da3fb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "99fe9ee9-8937-4ed7-95a3-ffd328abe2ad", "AQAAAAIAAYagAAAAEKuPDtZFCMxSON2T8ZQkKZ/hi0yU+xapVOabwjtOPbVQMXFrSkwAjGIvmkKe86ncdw==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d7930984-3648-45c8-b33e-7b902e1166b4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b03d864c-db49-40e1-9702-a872f7bb1c6d", "AQAAAAIAAYagAAAAEEJ9MCga/iUTsu7kW5V0yBKE3fpMCBD5HrTCx+6amFgFcmjtKdtOk9S4lqd57PGNZg==" });
        }
    }
}
