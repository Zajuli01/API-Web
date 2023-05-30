using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API_Web.Migrations
{
    /// <inheritdoc />
    public partial class addDefaultRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "tb_m_roles",
                columns: new[] { "guid", "created_date", "modified_date", "name" },
                values: new object[,]
                {
                    { new Guid("7a79c185-b182-4421-2071-08db60bf1afd"), new DateTime(2023, 5, 30, 11, 16, 59, 441, DateTimeKind.Local).AddTicks(5179), new DateTime(2023, 5, 30, 11, 16, 59, 441, DateTimeKind.Local).AddTicks(5181), "Admin" },
                    { new Guid("9e8b7346-0bdb-4dda-2072-08db60bf1afd"), new DateTime(2023, 5, 30, 11, 16, 59, 441, DateTimeKind.Local).AddTicks(5151), new DateTime(2023, 5, 30, 11, 16, 59, 441, DateTimeKind.Local).AddTicks(5168), "User" },
                    { new Guid("a62b543b-c06a-4d51-2073-08db60bf1afd"), new DateTime(2023, 5, 30, 11, 16, 59, 441, DateTimeKind.Local).AddTicks(5188), new DateTime(2023, 5, 30, 11, 16, 59, 441, DateTimeKind.Local).AddTicks(5190), "Manager" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("7a79c185-b182-4421-2071-08db60bf1afd"));

            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("9e8b7346-0bdb-4dda-2072-08db60bf1afd"));

            migrationBuilder.DeleteData(
                table: "tb_m_roles",
                keyColumn: "guid",
                keyValue: new Guid("a62b543b-c06a-4d51-2073-08db60bf1afd"));
        }
    }
}
