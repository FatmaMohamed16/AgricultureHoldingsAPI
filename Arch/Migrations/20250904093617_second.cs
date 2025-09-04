using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arch.Migrations
{
    /// <inheritdoc />
    public partial class second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Association",
                table: "AgriculturalHoldings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EasternBorder",
                table: "AgriculturalHoldings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Houd",
                table: "AgriculturalHoldings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NorthernBorder",
                table: "AgriculturalHoldings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SouthernBorder",
                table: "AgriculturalHoldings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WesternBorder",
                table: "AgriculturalHoldings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Association",
                table: "AgriculturalHoldings");

            migrationBuilder.DropColumn(
                name: "EasternBorder",
                table: "AgriculturalHoldings");

            migrationBuilder.DropColumn(
                name: "Houd",
                table: "AgriculturalHoldings");

            migrationBuilder.DropColumn(
                name: "NorthernBorder",
                table: "AgriculturalHoldings");

            migrationBuilder.DropColumn(
                name: "SouthernBorder",
                table: "AgriculturalHoldings");

            migrationBuilder.DropColumn(
                name: "WesternBorder",
                table: "AgriculturalHoldings");
        }
    }
}
