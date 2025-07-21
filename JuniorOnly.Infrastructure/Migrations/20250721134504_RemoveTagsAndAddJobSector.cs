using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuniorOnly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTagsAndAddJobSector : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OfferTag");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.AddColumn<Guid>(
                name: "JobSectorId",
                table: "Offers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CandidateProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CandidateProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "JobSectors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSectors", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Offers_JobSectorId",
                table: "Offers",
                column: "JobSectorId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSectors_Name",
                table: "JobSectors",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_JobSectors_JobSectorId",
                table: "Offers",
                column: "JobSectorId",
                principalTable: "JobSectors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_JobSectors_JobSectorId",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "JobSectors");

            migrationBuilder.DropIndex(
                name: "IX_Offers_JobSectorId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "JobSectorId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CandidateProfiles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CandidateProfiles");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferTag",
                columns: table => new
                {
                    OffersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfferTag", x => new { x.OffersId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_OfferTag_Offers_OffersId",
                        column: x => x.OffersId,
                        principalTable: "Offers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OfferTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OfferTag_TagsId",
                table: "OfferTag",
                column: "TagsId");
        }
    }
}
