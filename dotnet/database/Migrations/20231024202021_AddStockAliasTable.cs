using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database.Migrations
{
    /// <inheritdoc />
    public partial class AddStockAliasTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockAlias",
                columns: table => new
                {
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StockSymbol = table.Column<string>(type: "nvarchar(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockAlias", x => x.Description);
                    table.ForeignKey(
                        name: "FK_StockAlias_Stock_StockSymbol",
                        column: x => x.StockSymbol,
                        principalTable: "Stock",
                        principalColumn: "StockSymbol",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockAlias_StockSymbol",
                table: "StockAlias",
                column: "StockSymbol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockAlias");
        }
    }
}
