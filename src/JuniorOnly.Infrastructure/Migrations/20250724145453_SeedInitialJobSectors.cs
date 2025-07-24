using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JuniorOnly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialJobSectors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "JobSectors",
                columns: new[] { "Id", "CreatedAt", "IsActive", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), new DateTime(2025, 7, 24, 14, 47, 27, 0, DateTimeKind.Utc), true, "Santé", new DateTime(2025, 7, 24, 14, 47, 27, 0, DateTimeKind.Utc) },
                    { new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), new DateTime(2025, 7, 24, 14, 47, 27, 0, DateTimeKind.Utc), true, "Informatique", new DateTime(2025, 7, 24, 14, 47, 27, 0, DateTimeKind.Utc) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "JobSectors",
                keyColumn: "Id",
                keyValue: new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"));

            migrationBuilder.DeleteData(
                table: "JobSectors",
                keyColumn: "Id",
                keyValue: new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"));
        }
    }
}
