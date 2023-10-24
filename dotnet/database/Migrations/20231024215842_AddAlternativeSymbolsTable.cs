using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database.Migrations
{
    /// <inheritdoc />
    public partial class AddAlternativeSymbolsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlternativeSymbol",
                columns: table => new
                {
                    Alternative = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlternativeSymbol", x => x.Alternative);
                    table.ForeignKey(
                        name: "FK_AlternativeSymbol_Stock_StockSymbol",
                        column: x => x.StockSymbol,
                        principalTable: "Stock",
                        principalColumn: "StockSymbol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlternativeSymbol_StockSymbol",
                table: "AlternativeSymbol",
                column: "StockSymbol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlternativeSymbol");
        }
    }
}
