using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication4.Data.Migrations
{
    public partial class _25 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "customerName",
                table: "tblInvoice",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyName",
                table: "tblInvoice",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "Due_Date",
                table: "tblInvoice",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "tblCompany",
                columns: table => new
                {
                    CompanyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CompanyrCode = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Contact = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCompany", x => x.CompanyId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblInvoice_CompanyName",
                table: "tblInvoice",
                column: "CompanyName");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvoice_customerName",
                table: "tblInvoice",
                column: "customerName");

            migrationBuilder.AddForeignKey(
                name: "FK_tblInvoice_tblCompany_CompanyName",
                table: "tblInvoice",
                column: "CompanyName",
                principalTable: "tblCompany",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tblInvoice_Customer_customerName",
                table: "tblInvoice",
                column: "customerName",
                principalTable: "Customer",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblInvoice_tblCompany_CompanyName",
                table: "tblInvoice");

            migrationBuilder.DropForeignKey(
                name: "FK_tblInvoice_Customer_customerName",
                table: "tblInvoice");

            migrationBuilder.DropTable(
                name: "tblCompany");

            migrationBuilder.DropIndex(
                name: "IX_tblInvoice_CompanyName",
                table: "tblInvoice");

            migrationBuilder.DropIndex(
                name: "IX_tblInvoice_customerName",
                table: "tblInvoice");

            migrationBuilder.DropColumn(
                name: "Due_Date",
                table: "tblInvoice");

            migrationBuilder.AlterColumn<string>(
                name: "customerName",
                table: "tblInvoice",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "tblInvoice",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
