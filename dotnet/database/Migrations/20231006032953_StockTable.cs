using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database.Migrations
{
    /// <inheritdoc />
    public partial class StockTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StockSymbol",
                table: "StockTransaction",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    StockSymbol = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectToStampDuty = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.StockSymbol);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockTransaction_StockSymbol",
                table: "StockTransaction",
                column: "StockSymbol");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransaction_Stock_StockSymbol",
                table: "StockTransaction",
                column: "StockSymbol",
                principalTable: "Stock",
                principalColumn: "StockSymbol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransaction_Stock_StockSymbol",
                table: "StockTransaction");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropIndex(
                name: "IX_StockTransaction_StockSymbol",
                table: "StockTransaction");

            migrationBuilder.DropColumn(
                name: "StockSymbol",
                table: "StockTransaction");
        }
    }
}
