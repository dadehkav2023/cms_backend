using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations.identity
{
    public partial class addLocationTypeEnumtoCityOrVillage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationType",
                table: "CityOrVillage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationType",
                table: "CityOrVillage");
        }
    }
}
