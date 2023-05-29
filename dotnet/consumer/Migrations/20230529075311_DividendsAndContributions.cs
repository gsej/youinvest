using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace consumer.Migrations
{
    /// <inheritdoc />
    public partial class DividendsAndContributions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contributions",
                columns: table => new
                {
                    Account = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    TotalReceived = table.Column<decimal>(type: "decimal(19,5)", precision: 19, scale: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contributions", x => new { x.Account, x.Year });
                });

            migrationBuilder.CreateTable(
                name: "Dividends",
                columns: table => new
                {
                    Account = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Year = table.Column<int>(type: "int", nullable: false),
                    TotalReceived = table.Column<decimal>(type: "decimal(19,5)", precision: 19, scale: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dividends", x => new { x.Account, x.Year });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contributions");

            migrationBuilder.DropTable(
                name: "Dividends");
        }
    }
}
