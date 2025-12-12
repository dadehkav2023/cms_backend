using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations.identity
{
    public partial class add_wallet_and_financialtransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "financial");

            migrationBuilder.CreateTable(
                name: "FinancialTransaction",
                schema: "financial",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: true),
                    BankToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    BasketCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestAmount = table.Column<long>(type: "bigint", nullable: false),
                    BankResponseAsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResCode = table.Column<int>(type: "int", nullable: false),
                    RefNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TRACENO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurePan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VerifyPaymentResult = table.Column<double>(type: "float", nullable: false),
                    ReversePaymentResult = table.Column<double>(type: "float", nullable: false),
                    ReceiptFileFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InsertBy = table.Column<int>(type: "int", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: true),
                    RemoveTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<int>(type: "int", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialTransaction_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinancialTransaction_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransaction_OrderId",
                schema: "financial",
                table: "FinancialTransaction",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransaction_UserId",
                schema: "financial",
                table: "FinancialTransaction",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialTransaction",
                schema: "financial");
        }
    }
}
