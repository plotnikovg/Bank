using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Migration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Balance_Currency_Code",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Balance_Currency_Code",
                table: "BankAccounts");
        }
    }
}
