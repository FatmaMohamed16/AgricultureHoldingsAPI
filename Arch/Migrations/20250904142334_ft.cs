using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arch.Migrations
{
    /// <inheritdoc />
    public partial class ft : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgriculturalHoldings_User_UserId",
                table: "AgriculturalHoldings");

            migrationBuilder.DropIndex(
                name: "IX_AgriculturalHoldings_UserId",
                table: "AgriculturalHoldings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AgriculturalHoldings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "AgriculturalHoldings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AgriculturalHoldings_UserId",
                table: "AgriculturalHoldings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AgriculturalHoldings_User_UserId",
                table: "AgriculturalHoldings",
                column: "UserId",
                principalTable: "User",
                principalColumn: "ID");
        }
    }
}
