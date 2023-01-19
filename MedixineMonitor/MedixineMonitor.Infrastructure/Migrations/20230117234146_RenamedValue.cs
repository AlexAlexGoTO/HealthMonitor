using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedixineMonitor.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenamedValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Observations",
                newName: "Value");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Observations",
                newName: "Number");
        }
    }
}
