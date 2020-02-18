using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication4.Data.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblAccountHead",
                columns: table => new
                {
                    accountHeadId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    accountHeadName = table.Column<string>(nullable: false),
                    account_Head_Code = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAccountHead", x => x.accountHeadId);
                });

            migrationBuilder.CreateTable(
                name: "tblItemcategory",
                columns: table => new
                {
                    catId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    catName = table.Column<string>(nullable: false),
                    catDesc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemcategory", x => x.catId);
                });

            migrationBuilder.CreateTable(
                name: "tblItemUnit",
                columns: table => new
                {
                    unitId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    unitName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItemUnit", x => x.unitId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    userId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    username = table.Column<string>(nullable: false),
                    password = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.userId);
                });

            migrationBuilder.CreateTable(
                name: "tblAccount",
                columns: table => new
                {
                    accountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    accountHeadId = table.Column<int>(nullable: false),
                    accountCode = table.Column<int>(nullable: false),
                    accountTitle = table.Column<string>(nullable: false),
                    PhoneNo = table.Column<string>(nullable: true),
                    MobileNo = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAccount", x => x.accountId);
                    table.ForeignKey(
                        name: "FK_tblAccount_tblAccountHead_accountHeadId",
                        column: x => x.accountHeadId,
                        principalTable: "tblAccountHead",
                        principalColumn: "accountHeadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblItem",
                columns: table => new
                {
                    itemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    catId = table.Column<int>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    ItemCode = table.Column<string>(nullable: false),
                    itemName = table.Column<string>(nullable: false),
                    purchase_Price = table.Column<decimal>(nullable: false),
                    sale_Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblItem", x => x.itemId);
                    table.ForeignKey(
                        name: "FK_tblItem_tblItemUnit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "tblItemUnit",
                        principalColumn: "unitId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblItem_tblItemcategory_catId",
                        column: x => x.catId,
                        principalTable: "tblItemcategory",
                        principalColumn: "catId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblInvoice",
                columns: table => new
                {
                    invoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    invoice_Code = table.Column<string>(nullable: false),
                    Invoice_type = table.Column<int>(nullable: false),
                    payment_Mode = table.Column<int>(nullable: false),
                    invoice_Date = table.Column<DateTime>(nullable: false),
                    CompanyName = table.Column<string>(nullable: false),
                    accountId = table.Column<int>(nullable: false),
                    customerName = table.Column<string>(nullable: true),
                    Created_Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInvoice", x => x.invoiceId);
                    table.ForeignKey(
                        name: "FK_tblInvoice_tblAccount_accountId",
                        column: x => x.accountId,
                        principalTable: "tblAccount",
                        principalColumn: "accountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblInvoiceDetail",
                columns: table => new
                {
                    invoiceDetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    invoiceId = table.Column<int>(nullable: false),
                    itemId = table.Column<int>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInvoiceDetail", x => x.invoiceDetailId);
                    table.ForeignKey(
                        name: "FK_tblInvoiceDetail_tblInvoice_invoiceId",
                        column: x => x.invoiceId,
                        principalTable: "tblInvoice",
                        principalColumn: "invoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblInvoiceDetail_tblItem_itemId",
                        column: x => x.itemId,
                        principalTable: "tblItem",
                        principalColumn: "itemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblVoucher",
                columns: table => new
                {
                    voucherId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    voucherCode = table.Column<string>(nullable: false),
                    voucherDate = table.Column<DateTime>(nullable: false),
                    invoiceId = table.Column<int>(nullable: false),
                    userId = table.Column<int>(nullable: false),
                    createdDate = table.Column<DateTime>(nullable: false),
                    voucher_Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVoucher", x => x.voucherId);
                    table.ForeignKey(
                        name: "FK_tblVoucher_tblInvoice_invoiceId",
                        column: x => x.invoiceId,
                        principalTable: "tblInvoice",
                        principalColumn: "invoiceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblVoucherDetail",
                columns: table => new
                {
                    voucherdetailId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    voucherId = table.Column<int>(nullable: false),
                    Narration = table.Column<string>(nullable: true),
                    debitAmount = table.Column<decimal>(nullable: false),
                    creditAmount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVoucherDetail", x => x.voucherdetailId);
                    table.ForeignKey(
                        name: "FK_tblVoucherDetail_tblVoucher_voucherId",
                        column: x => x.voucherId,
                        principalTable: "tblVoucher",
                        principalColumn: "voucherId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblAccount_accountHeadId",
                table: "tblAccount",
                column: "accountHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvoice_accountId",
                table: "tblInvoice",
                column: "accountId");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvoiceDetail_invoiceId",
                table: "tblInvoiceDetail",
                column: "invoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvoiceDetail_itemId",
                table: "tblInvoiceDetail",
                column: "itemId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItem_UnitId",
                table: "tblItem",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItem_catId",
                table: "tblItem",
                column: "catId");

            migrationBuilder.CreateIndex(
                name: "IX_tblVoucher_invoiceId",
                table: "tblVoucher",
                column: "invoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblVoucherDetail_voucherId",
                table: "tblVoucherDetail",
                column: "voucherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblInvoiceDetail");

            migrationBuilder.DropTable(
                name: "tblVoucherDetail");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "tblItem");

            migrationBuilder.DropTable(
                name: "tblVoucher");

            migrationBuilder.DropTable(
                name: "tblItemUnit");

            migrationBuilder.DropTable(
                name: "tblItemcategory");

            migrationBuilder.DropTable(
                name: "tblInvoice");

            migrationBuilder.DropTable(
                name: "tblAccount");

            migrationBuilder.DropTable(
                name: "tblAccountHead");
        }
    }
}
