using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace consumer.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountToStockTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Account",
                table: "StockTransaction",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Account",
                table: "StockTransaction");
        }
    }
}
