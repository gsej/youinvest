using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database.Migrations
{
    /// <inheritdoc />
    public partial class AccountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Account",
                table: "StockTransaction",
                newName: "AccountCode");

            migrationBuilder.RenameColumn(
                name: "Account",
                table: "CashStatementItem",
                newName: "AccountCode");

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountCode);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_AccountCode",
                table: "StockTransaction",
                column: "AccountCode");

            migrationBuilder.CreateIndex(
                name: "IX_CashStatementItem_AccountCode",
                table: "CashStatementItem",
                column: "AccountCode");

            migrationBuilder.AddForeignKey(
                name: "FK_CashStatementItem_Account_AccountCode",
                table: "CashStatementItem",
                column: "AccountCode",
                principalTable: "Account",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_Account_AccountCode",
                table: "StockTransaction",
                column: "AccountCode",
                principalTable: "Account",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CashStatementItem_Account_AccountCode",
                table: "CashStatementItem");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_Account_AccountCode",
                table: "StockTransaction");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropIndex(
                name: "IX_StockTransaction_AccountCode",
                table: "StockTransaction");

            migrationBuilder.DropIndex(
                name: "IX_CashStatementItem_AccountCode",
                table: "CashStatementItem");

            migrationBuilder.RenameColumn(
                name: "AccountCode",
                table: "StockTransaction",
                newName: "Account");

            migrationBuilder.RenameColumn(
                name: "AccountCode",
                table: "CashStatementItem",
                newName: "Account");
        }
    }
}
