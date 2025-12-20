using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SolarPanelInstallationManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "consumer_survey",
                columns: table => new
                {
                    Sno = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    District = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Constituency = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Mandal = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Village = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ConsumerNameWithSurname = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    USCNO = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ExistingContractedLoadKW = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    HasOtherDomesticServices = table.Column<bool>(type: "boolean", nullable: true),
                    ConsumerMobileNo = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    AadharNo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    IsBeneficiaryUnderGJScheme = table.Column<bool>(type: "boolean", nullable: true),
                    LatitudeLongitude = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    TypeOfRoof = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ShadeFreeRoofAreaSqFt = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    Remarks = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsNameChangeRequired = table.Column<bool>(type: "boolean", nullable: true),
                    SSName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    FeederName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    ServiceNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PoleNumber = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    DtrStructureCode = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    ExistingDtrCapacityKva = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consumer_survey", x => x.Sno);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "consumer_survey");
        }
    }
}
