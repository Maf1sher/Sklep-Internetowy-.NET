using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sklep_Internetowy_.NET.Migrations
{
    /// <inheritdoc />
    public partial class ProductQuantity5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Orders_OrderId1",
                table: "OrderProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProduct_Products_ProductId1",
                table: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_OrderProduct_OrderId1",
                table: "OrderProduct");

            migrationBuilder.DropIndex(
                name: "IX_OrderProduct_ProductId1",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "OrderProduct");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "OrderProduct");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId1",
                table: "OrderProduct",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                table: "OrderProduct",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_OrderId1",
                table: "OrderProduct",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductId1",
                table: "OrderProduct",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Orders_OrderId1",
                table: "OrderProduct",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProduct_Products_ProductId1",
                table: "OrderProduct",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
