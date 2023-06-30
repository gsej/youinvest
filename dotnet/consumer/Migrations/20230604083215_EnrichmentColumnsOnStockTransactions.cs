using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace consumer.Migrations
{
    /// <inheritdoc />
    public partial class EnrichmentColumnsOnStockTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                table: "StockTransaction",
                type: "decimal(19,5)",
                precision: 19,
                scale: 5,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StampDuty",
                table: "StockTransaction",
                type: "decimal(19,5)",
                precision: 19,
                scale: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "StockTransaction",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fee",
                table: "StockTransaction");

            migrationBuilder.DropColumn(
                name: "StampDuty",
                table: "StockTransaction");

            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "StockTransaction");
        }
    }
}
