using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sklep_Internetowy_.NET.Migrations
{
    /// <inheritdoc />
    public partial class ProductCreateDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedData",
                table: "Products",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedData",
                table: "Products");
        }
    }
}
