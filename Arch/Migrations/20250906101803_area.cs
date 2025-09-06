using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arch.Migrations
{
    /// <inheritdoc />
    public partial class area : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActualArea",
                table: "AgriculturalHoldings",
                newName: "ActualAreaInSquareMeters");

            migrationBuilder.AddColumn<int>(
                name: "Faddan",
                table: "AgriculturalHoldings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Qirat",
                table: "AgriculturalHoldings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Sahm",
                table: "AgriculturalHoldings",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Faddan",
                table: "AgriculturalHoldings");

            migrationBuilder.DropColumn(
                name: "Qirat",
                table: "AgriculturalHoldings");

            migrationBuilder.DropColumn(
                name: "Sahm",
                table: "AgriculturalHoldings");

            migrationBuilder.RenameColumn(
                name: "ActualAreaInSquareMeters",
                table: "AgriculturalHoldings",
                newName: "ActualArea");
        }
    }
}
