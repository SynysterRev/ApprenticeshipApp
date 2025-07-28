using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JuniorOnly.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrectTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ApplieddAt",
                table: "Applications",
                newName: "AppliedAt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppliedAt",
                table: "Applications",
                newName: "ApplieddAt");
        }
    }
}
