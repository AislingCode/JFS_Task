using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JFS_Task.Migrations
{
    public partial class postgresqlMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Balance",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    Period = table.Column<int>(type: "integer", nullable: false),
                    InBalance = table.Column<double>(type: "double precision", nullable: false),
                    Calculation = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Sum = table.Column<double>(type: "double precision", nullable: false),
                    PaymentGuid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Balance");

            migrationBuilder.DropTable(
                name: "Payment");
        }
    }
}
