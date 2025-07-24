using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuniorOnly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSalaryRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalaryMax",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalaryMin",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SalaryPeriod",
                table: "Offers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "CandidateProfiles",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Offer_SalaryRange",
                table: "Offers",
                sql: "[SalaryMax] > [SalaryMin]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Offer_SalaryRange",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "SalaryMax",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "SalaryMin",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "SalaryPeriod",
                table: "Offers");

            migrationBuilder.AlterColumn<string>(
                name: "Location",
                table: "CandidateProfiles",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);
        }
    }
}
