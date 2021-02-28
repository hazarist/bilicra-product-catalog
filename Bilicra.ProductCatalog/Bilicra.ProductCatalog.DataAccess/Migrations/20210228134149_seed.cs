using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bilicra.ProductCatalog.DataAccess.Migrations
{
    public partial class seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Code", "CreatedDate", "Currency", "IsConfirmed", "IsDeleted", "LastUpdatedDate", "Name", "Photo", "Price" },
                values: new object[,]
                {
                    { new Guid("b4512880-ecb2-444c-8c03-2908ab51892b"), "testCode", new DateTime(2021, 2, 28, 16, 41, 49, 407, DateTimeKind.Local).AddTicks(5620), 0, true, false, null, "test product", null, 55m },
                    { new Guid("698f932e-dcf6-4580-a5ca-5d572ade4765"), "testCode2", new DateTime(2021, 2, 28, 16, 41, 49, 408, DateTimeKind.Local).AddTicks(5174), 0, false, false, null, "test product 2", null, 1000m }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "IsDeleted", "LastUpdatedDate", "Name", "Password", "PhoneNumber", "Salt", "Surname", "Username" },
                values: new object[] { new Guid("b624b02c-4337-44fc-8aad-6bbefb7ed8e6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "user.test@gmail.com", false, null, "user", "OUQbMtDPGRPjcQlPLR0N+H4pwBj0RKx7sHH9lySmNww=", null, "KUDTfNFTrOMjdx5GtmpLYQ==", "test", "test" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("698f932e-dcf6-4580-a5ca-5d572ade4765"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("b4512880-ecb2-444c-8c03-2908ab51892b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("b624b02c-4337-44fc-8aad-6bbefb7ed8e6"));
        }
    }
}
