using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Balance_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WithdrawalLimit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsBlocked = table.Column<bool>(type: "bit", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankCard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCard", x => new { x.BankAccountId, x.Id });
                    table.ForeignKey(
                        name: "FK_BankCard_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Passport_Name_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passport_Name_SecondName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Passport_Name_LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passport_Name_Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passport_Series = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passport_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passport_RegistrationAddress_RegistrationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Passport_RegistrationAddress_Region = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passport_RegistrationAddress_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passport_RegistrationAddress_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passport_RegistrationAddress_HouseNumber = table.Column<int>(type: "int", nullable: false),
                    Passport_RegistrationAddress_BuildingNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountForReceivingTransfersId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_BankAccounts_AccountForReceivingTransfersId",
                        column: x => x.AccountForReceivingTransfersId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_ClientId",
                table: "BankAccounts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_AccountForReceivingTransfersId",
                table: "Clients",
                column: "AccountForReceivingTransfersId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Clients_ClientId",
                table: "BankAccounts",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Clients_ClientId",
                table: "BankAccounts");

            migrationBuilder.DropTable(
                name: "BankCard");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "BankAccounts");
        }
    }
}
