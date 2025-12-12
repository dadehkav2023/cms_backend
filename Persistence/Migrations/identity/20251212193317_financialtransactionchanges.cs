using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations.identity
{
    public partial class financialtransactionchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasketCode",
                schema: "financial",
                table: "FinancialTransaction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BasketCode",
                schema: "financial",
                table: "FinancialTransaction",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
