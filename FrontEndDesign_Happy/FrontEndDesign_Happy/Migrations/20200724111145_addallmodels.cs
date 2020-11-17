using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FrontEndDesign_Happy.Migrations
{
    public partial class addallmodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_M_Buyer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Buyer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Stock = table.Column<int>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    categoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_M_Item_TB_M_Category_categoryId",
                        column: x => x.categoryId,
                        principalTable: "TB_M_Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Receipt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OrderDate = table.Column<DateTimeOffset>(nullable: false),
                    itemId = table.Column<int>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<int>(nullable: false),
                    buyerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Receipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_M_Receipt_TB_M_Buyer_buyerId",
                        column: x => x.buyerId,
                        principalTable: "TB_M_Buyer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TB_M_Receipt_TB_M_Item_itemId",
                        column: x => x.itemId,
                        principalTable: "TB_M_Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TB_M_Checkout",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    receiptId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_M_Checkout", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TB_M_Checkout_TB_M_Receipt_receiptId",
                        column: x => x.receiptId,
                        principalTable: "TB_M_Receipt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Checkout_receiptId",
                table: "TB_M_Checkout",
                column: "receiptId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Item_categoryId",
                table: "TB_M_Item",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Receipt_buyerId",
                table: "TB_M_Receipt",
                column: "buyerId");

            migrationBuilder.CreateIndex(
                name: "IX_TB_M_Receipt_itemId",
                table: "TB_M_Receipt",
                column: "itemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_M_Checkout");

            migrationBuilder.DropTable(
                name: "TB_M_Receipt");

            migrationBuilder.DropTable(
                name: "TB_M_Buyer");

            migrationBuilder.DropTable(
                name: "TB_M_Item");

            migrationBuilder.DropTable(
                name: "TB_M_Category");
        }
    }
}
