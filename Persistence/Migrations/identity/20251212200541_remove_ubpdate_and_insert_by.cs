using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations.identity
{
    public partial class remove_ubpdate_and_insert_by : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InsertBy",
                schema: "financial",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                schema: "financial",
                table: "Wallet");

            migrationBuilder.DropColumn(
                name: "InsertBy",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "InsertBy",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "InsertBy",
                table: "Part");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "Part");

            migrationBuilder.DropColumn(
                name: "InsertBy",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "OrderItem");

            migrationBuilder.DropColumn(
                name: "InsertBy",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "InsertBy",
                schema: "financial",
                table: "FinancialTransaction");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                schema: "financial",
                table: "FinancialTransaction");

            migrationBuilder.DropColumn(
                name: "InsertBy",
                table: "County");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "County");

            migrationBuilder.DropColumn(
                name: "InsertBy",
                table: "CityOrVillage");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "CityOrVillage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InsertBy",
                schema: "financial",
                table: "Wallet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateBy",
                schema: "financial",
                table: "Wallet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsertBy",
                table: "Province",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateBy",
                table: "Province",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsertBy",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateBy",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsertBy",
                table: "Part",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateBy",
                table: "Part",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsertBy",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateBy",
                table: "OrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsertBy",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateBy",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsertBy",
                schema: "financial",
                table: "FinancialTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateBy",
                schema: "financial",
                table: "FinancialTransaction",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsertBy",
                table: "County",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateBy",
                table: "County",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InsertBy",
                table: "CityOrVillage",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdateBy",
                table: "CityOrVillage",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
