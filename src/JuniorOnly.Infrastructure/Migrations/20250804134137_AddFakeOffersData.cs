using JuniorOnly.Domain.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JuniorOnly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFakeOffersData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var userId = new Guid("11111111-1111-1111-1111-111111111111");
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            var user = new ApplicationUser
            {
                Id = userId,
                UserName = "admin@example.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                FirstName = "Admin",
                LastName = "User",
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            var passwordHash = passwordHasher.HashPassword(user, "P@ssw0rd!");
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "Email", "NormalizedEmail", "EmailConfirmed", "PasswordHash", "FirstName", "LastName", "CreatedAt", "UpdatedAt", "SecurityStamp", "ConcurrencyStamp", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "AccessFailedCount" },
                values: new object[] {
            user.Id,
            user.UserName,
            user.NormalizedUserName,
            user.Email,
            user.NormalizedEmail,
            user.EmailConfirmed,
            passwordHash,
            user.FirstName,
            user.LastName,
            user.CreatedAt,
            user.UpdatedAt,
            Guid.NewGuid().ToString(),
            Guid.NewGuid().ToString(),
            false,
            false, 
            false,
            0 
                }
            );


            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Name", "CreatedByUserId", "CreatedAt", "UpdatedAt", "IsReconversionFriendly", "Description", "LogoUrl", "Website" },
                values: new object[]
                {
                    new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"),
                    "Entreprise",
                    new Guid("11111111-1111-1111-1111-111111111111"),
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    false,
                    "Description",
                    null,
                    null
                }
            );

            migrationBuilder.InsertData(
                table: "Offers",
                columns: new[] { "Id", "CompanyId", "ContractType", "DeletedAt", "Description", "ExperienceRequired", "IsDeleted", "JobSectorId", "Location", "PublishedAt", "RemoteType", "SalaryMax", "SalaryMin", "SalaryPeriod", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 0", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 0", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 50000, 20000, 0, "Title 0", new DateTime(2025, 8, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 1", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 1", new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 51000, 21000, 1, "Title 1", new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 2", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 2", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 52000, 22000, 2, "Title 2", new DateTime(2025, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 3", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 3", new DateTime(2025, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 53000, 23000, 0, "Title 3", new DateTime(2025, 8, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 4", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 4", new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 54000, 24000, 1, "Title 4", new DateTime(2025, 8, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 5", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 5", new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 55000, 25000, 2, "Title 5", new DateTime(2025, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 6", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 6", new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 56000, 26000, 0, "Title 6", new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 7", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 7", new DateTime(2025, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 57000, 27000, 1, "Title 7", new DateTime(2025, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 8", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 8", new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 58000, 28000, 2, "Title 8", new DateTime(2025, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 9", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 9", new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 59000, 29000, 0, "Title 9", new DateTime(2025, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000011"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 10", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 10", new DateTime(2025, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 60000, 30000, 1, "Title 10", new DateTime(2025, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000012"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 11", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 11", new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 61000, 31000, 2, "Title 11", new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000013"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 12", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 12", new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 62000, 32000, 0, "Title 12", new DateTime(2025, 8, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000014"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 13", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 13", new DateTime(2025, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 63000, 33000, 1, "Title 13", new DateTime(2025, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000015"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 14", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 14", new DateTime(2025, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 64000, 34000, 2, "Title 14", new DateTime(2025, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000016"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 15", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 15", new DateTime(2025, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 65000, 35000, 0, "Title 15", new DateTime(2025, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000017"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 16", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 16", new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 66000, 36000, 1, "Title 16", new DateTime(2025, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000018"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 17", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 17", new DateTime(2025, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 67000, 37000, 2, "Title 17", new DateTime(2025, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000019"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 18", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 18", new DateTime(2025, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 68000, 38000, 0, "Title 18", new DateTime(2025, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000020"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 19", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 19", new DateTime(2025, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 69000, 39000, 1, "Title 19", new DateTime(2025, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000021"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 20", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 20", new DateTime(2025, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 70000, 40000, 2, "Title 20", new DateTime(2025, 8, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000022"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 21", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 21", new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 71000, 41000, 0, "Title 21", new DateTime(2025, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000023"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 22", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 22", new DateTime(2025, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 72000, 42000, 1, "Title 22", new DateTime(2025, 8, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000024"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 23", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 23", new DateTime(2025, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 73000, 43000, 2, "Title 23", new DateTime(2025, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000025"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 24", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 24", new DateTime(2025, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 74000, 44000, 0, "Title 24", new DateTime(2025, 8, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000026"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 25", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 25", new DateTime(2025, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 75000, 45000, 1, "Title 25", new DateTime(2025, 8, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000027"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 26", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 26", new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 76000, 46000, 2, "Title 26", new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000028"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 27", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 27", new DateTime(2025, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 77000, 47000, 0, "Title 27", new DateTime(2025, 8, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000029"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 28", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 28", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 78000, 48000, 1, "Title 28", new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000030"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 29", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 29", new DateTime(2025, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 79000, 49000, 2, "Title 29", new DateTime(2025, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000031"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 30", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 30", new DateTime(2025, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 80000, 50000, 0, "Title 30", new DateTime(2025, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000032"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 31", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 31", new DateTime(2025, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 81000, 51000, 1, "Title 31", new DateTime(2025, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000033"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 32", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 32", new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 82000, 52000, 2, "Title 32", new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000034"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 33", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 33", new DateTime(2025, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 83000, 53000, 0, "Title 33", new DateTime(2025, 9, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000035"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 34", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 34", new DateTime(2025, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 84000, 54000, 1, "Title 34", new DateTime(2025, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000036"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 35", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 35", new DateTime(2025, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 85000, 55000, 2, "Title 35", new DateTime(2025, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000037"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 36", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 36", new DateTime(2025, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 86000, 56000, 0, "Title 36", new DateTime(2025, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000038"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 37", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 37", new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 87000, 57000, 1, "Title 37", new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000039"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 38", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 38", new DateTime(2025, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 88000, 58000, 2, "Title 38", new DateTime(2025, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 39", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 39", new DateTime(2025, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 89000, 59000, 0, "Title 39", new DateTime(2025, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000041"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 40", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 40", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 90000, 60000, 1, "Title 40", new DateTime(2025, 9, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000042"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 41", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 41", new DateTime(2025, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 91000, 61000, 2, "Title 41", new DateTime(2025, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000043"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 42", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 42", new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 92000, 62000, 0, "Title 42", new DateTime(2025, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000044"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 43", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 43", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 93000, 63000, 1, "Title 43", new DateTime(2025, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000045"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 44", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 44", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 94000, 64000, 2, "Title 44", new DateTime(2025, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000046"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 45", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 45", new DateTime(2025, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 95000, 65000, 0, "Title 45", new DateTime(2025, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000047"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 46", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 46", new DateTime(2025, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 96000, 66000, 1, "Title 46", new DateTime(2025, 9, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000048"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 47", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 47", new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 97000, 67000, 2, "Title 47", new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000049"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 48", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 48", new DateTime(2025, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 98000, 68000, 0, "Title 48", new DateTime(2025, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000050"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 49", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 49", new DateTime(2025, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 99000, 69000, 1, "Title 49", new DateTime(2025, 9, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000051"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 50", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 50", new DateTime(2025, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 100000, 70000, 2, "Title 50", new DateTime(2025, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000052"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 51", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 51", new DateTime(2025, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 101000, 71000, 0, "Title 51", new DateTime(2025, 9, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000053"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 52", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 52", new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 102000, 72000, 1, "Title 52", new DateTime(2025, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000054"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 53", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 53", new DateTime(2025, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 103000, 73000, 2, "Title 53", new DateTime(2025, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000055"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 54", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 54", new DateTime(2025, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 104000, 74000, 0, "Title 54", new DateTime(2025, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000056"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 55", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 55", new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 105000, 75000, 1, "Title 55", new DateTime(2025, 9, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000057"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 56", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 56", new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 106000, 76000, 2, "Title 56", new DateTime(2025, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000058"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 57", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 57", new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 107000, 77000, 0, "Title 57", new DateTime(2025, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000059"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 58", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 58", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 108000, 78000, 1, "Title 58", new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000060"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 59", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 59", new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 109000, 79000, 2, "Title 59", new DateTime(2025, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000061"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 60", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 60", new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 110000, 80000, 0, "Title 60", new DateTime(2025, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000062"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 61", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 61", new DateTime(2025, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 111000, 81000, 1, "Title 61", new DateTime(2025, 10, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000063"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 62", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 62", new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 112000, 82000, 2, "Title 62", new DateTime(2025, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000064"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 63", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 63", new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 113000, 83000, 0, "Title 63", new DateTime(2025, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000065"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 64", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 64", new DateTime(2025, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 114000, 84000, 1, "Title 64", new DateTime(2025, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000066"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 65", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 65", new DateTime(2025, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 115000, 85000, 2, "Title 65", new DateTime(2025, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000067"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 66", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 66", new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 116000, 86000, 0, "Title 66", new DateTime(2025, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000068"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 67", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 67", new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 117000, 87000, 1, "Title 67", new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000069"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 68", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 68", new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 118000, 88000, 2, "Title 68", new DateTime(2025, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000070"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 69", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 69", new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 119000, 89000, 0, "Title 69", new DateTime(2025, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000071"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 70", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 70", new DateTime(2025, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 120000, 90000, 1, "Title 70", new DateTime(2025, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000072"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 71", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 71", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 121000, 91000, 2, "Title 71", new DateTime(2025, 10, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000073"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 72", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 72", new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 122000, 92000, 0, "Title 72", new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000074"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 73", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 73", new DateTime(2025, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 123000, 93000, 1, "Title 73", new DateTime(2025, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000075"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 74", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 74", new DateTime(2025, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 124000, 94000, 2, "Title 74", new DateTime(2025, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000076"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 75", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 75", new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 125000, 95000, 0, "Title 75", new DateTime(2025, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000077"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 76", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 76", new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 126000, 96000, 1, "Title 76", new DateTime(2025, 10, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000078"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 77", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 77", new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 127000, 97000, 2, "Title 77", new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000079"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 78", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 78", new DateTime(2025, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 128000, 98000, 0, "Title 78", new DateTime(2025, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000080"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 79", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 79", new DateTime(2025, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 129000, 99000, 1, "Title 79", new DateTime(2025, 10, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000081"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 80", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 80", new DateTime(2025, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 130000, 100000, 2, "Title 80", new DateTime(2025, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000082"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 81", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 81", new DateTime(2025, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 131000, 101000, 0, "Title 81", new DateTime(2025, 10, 24, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000083"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 82", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 82", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 132000, 102000, 1, "Title 82", new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000084"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 83", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 83", new DateTime(2025, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 133000, 103000, 2, "Title 83", new DateTime(2025, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000085"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 84", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 84", new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 134000, 104000, 0, "Title 84", new DateTime(2025, 10, 27, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000086"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 85", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 85", new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 135000, 105000, 1, "Title 85", new DateTime(2025, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000087"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 86", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 86", new DateTime(2025, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 136000, 106000, 2, "Title 86", new DateTime(2025, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000088"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 87", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 87", new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 137000, 107000, 0, "Title 87", new DateTime(2025, 10, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000089"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 88", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 88", new DateTime(2025, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 138000, 108000, 1, "Title 88", new DateTime(2025, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000090"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 89", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 89", new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 139000, 109000, 2, "Title 89", new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000091"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 90", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 90", new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 140000, 110000, 0, "Title 90", new DateTime(2025, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000092"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 91", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 91", new DateTime(2025, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 141000, 111000, 1, "Title 91", new DateTime(2025, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000093"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 92", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 92", new DateTime(2025, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 142000, 112000, 2, "Title 92", new DateTime(2025, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000094"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 93", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 93", new DateTime(2025, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 143000, 113000, 0, "Title 93", new DateTime(2025, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000095"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 94", 1, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 94", new DateTime(2025, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 144000, 114000, 1, "Title 94", new DateTime(2025, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000096"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 0, null, "Description 95", 2, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 95", new DateTime(2025, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 145000, 115000, 2, "Title 95", new DateTime(2025, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000097"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 1, null, "Description 96", 0, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 96", new DateTime(2025, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 146000, 116000, 0, "Title 96", new DateTime(2025, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000098"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 2, null, "Description 97", 1, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 97", new DateTime(2025, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 147000, 117000, 1, "Title 97", new DateTime(2025, 11, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000099"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 3, null, "Description 98", 2, false, new Guid("2338d5aa-1b27-4bfe-85f7-4903e6d3434a"), "Location 98", new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 148000, 118000, 2, "Title 98", new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("00000000-0000-0000-0000-000000000100"), new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"), 4, null, "Description 99", 0, false, new Guid("0353cc9b-8a46-4cb4-bd53-e4204c8c7f0d"), "Location 99", new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 149000, 119000, 0, "Title 99", new DateTime(2025, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationUser",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: new Guid("badc08ad-c99b-406d-8ea4-7f0585be6a5f"));


            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000005"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000006"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000007"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000008"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000009"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000010"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000011"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000012"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000013"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000014"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000015"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000016"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000017"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000018"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000019"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000020"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000021"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000022"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000023"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000024"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000025"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000026"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000027"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000028"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000029"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000030"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000031"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000032"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000033"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000034"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000035"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000036"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000037"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000038"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000039"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000040"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000041"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000042"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000043"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000044"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000045"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000046"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000047"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000048"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000049"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000050"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000051"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000052"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000053"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000054"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000055"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000056"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000057"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000058"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000059"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000060"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000061"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000062"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000063"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000064"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000065"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000066"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000067"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000068"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000069"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000070"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000071"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000072"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000073"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000074"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000075"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000076"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000077"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000078"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000079"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000080"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000081"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000082"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000083"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000084"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000085"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000086"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000087"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000088"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000089"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000090"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000091"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000092"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000093"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000094"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000095"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000096"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000097"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000098"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000099"));

            migrationBuilder.DeleteData(
                table: "Offers",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000100"));
        }
    }
}
