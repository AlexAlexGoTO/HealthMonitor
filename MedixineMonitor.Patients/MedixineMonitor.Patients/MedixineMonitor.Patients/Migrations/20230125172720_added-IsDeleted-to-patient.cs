using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedixineMonitor.Patients.Migrations
{
    /// <inheritdoc />
    public partial class addedIsDeletedtopatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "patients",
                table: "Patient",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "patients",
                table: "Patient");
        }
    }
}
