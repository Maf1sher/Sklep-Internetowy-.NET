using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sklep_Internetowy_.NET.Migrations
{
    /// <inheritdoc />
    public partial class ProductSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnSale",
                table: "Products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PreviousPrice",
                table: "Products",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnSale",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PreviousPrice",
                table: "Products");
        }
    }
}
