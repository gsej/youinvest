using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database.Migrations
{
    /// <inheritdoc />
    public partial class RemoveStockPriceForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockPrice_Stock_StockSymbol",
                table: "StockPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_StockPrice_Stock_StockSymbol",
                table: "StockPrice",
                column: "StockSymbol",
                principalTable: "Stock",
                principalColumn: "StockSymbol",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
