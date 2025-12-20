using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SolarPanelInstallationManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsDeleteColumnAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "consumer_survey",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "consumer_survey");
        }
    }
}
