using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace database.Migrations
{
    /// <inheritdoc />
    public partial class MakeCurrencyNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "StockPrice",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AccountCode",
                table: "KnownValue",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.CreateIndex(
                name: "IX_KnownValue_AccountCode",
                table: "KnownValue",
                column: "AccountCode");

            migrationBuilder.AddForeignKey(
                name: "FK_KnownValue_Account_AccountCode",
                table: "KnownValue",
                column: "AccountCode",
                principalTable: "Account",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KnownValue_Account_AccountCode",
                table: "KnownValue");

            migrationBuilder.DropIndex(
                name: "IX_KnownValue_AccountCode",
                table: "KnownValue");

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "StockPrice",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "AccountCode",
                table: "KnownValue",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);
        }
    }
}
