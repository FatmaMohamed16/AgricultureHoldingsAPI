using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arch.Migrations
{
    /// <inheritdoc />
    public partial class MadinaMaglasId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MadinaMaglasId",
                table: "AgriculturalHoldings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AgriculturalHoldings_MadinaMaglasId",
                table: "AgriculturalHoldings",
                column: "MadinaMaglasId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgriculturalHoldings_Madina_Maglas_MadinaMaglasId",
                table: "AgriculturalHoldings",
                column: "MadinaMaglasId",
                principalTable: "Madina_Maglas",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgriculturalHoldings_Madina_Maglas_MadinaMaglasId",
                table: "AgriculturalHoldings");

            migrationBuilder.DropIndex(
                name: "IX_AgriculturalHoldings_MadinaMaglasId",
                table: "AgriculturalHoldings");

            migrationBuilder.DropColumn(
                name: "MadinaMaglasId",
                table: "AgriculturalHoldings");
        }
    }
}
