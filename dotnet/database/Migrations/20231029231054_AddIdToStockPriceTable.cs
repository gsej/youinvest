using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database.Migrations
{
    /// <inheritdoc />
    public partial class AddIdToStockPriceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StockPrice",
                table: "StockPrice");

            migrationBuilder.AddColumn<Guid>(
                name: "StockPriceId",
                table: "StockPrice",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockPrice",
                table: "StockPrice",
                column: "StockPriceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StockPrice",
                table: "StockPrice");

            migrationBuilder.DropColumn(
                name: "StockPriceId",
                table: "StockPrice");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StockPrice",
                table: "StockPrice",
                columns: new[] { "StockSymbol", "Date" });
        }
    }
}
