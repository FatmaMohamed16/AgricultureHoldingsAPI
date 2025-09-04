using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arch.Migrations
{
    /// <inheritdoc />
    public partial class four : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgriculturalHoldings_SourceOfOwnership_SourceOfOwnershipId",
                table: "AgriculturalHoldings");

            migrationBuilder.AlterColumn<int>(
                name: "SourceOfOwnershipId",
                table: "AgriculturalHoldings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "AgriculturalHoldings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AgriculturalHoldings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AgriculturalHoldings_SourceOfOwnership_SourceOfOwnershipId",
                table: "AgriculturalHoldings",
                column: "SourceOfOwnershipId",
                principalTable: "SourceOfOwnership",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgriculturalHoldings_SourceOfOwnership_SourceOfOwnershipId",
                table: "AgriculturalHoldings");

            migrationBuilder.AlterColumn<int>(
                name: "SourceOfOwnershipId",
                table: "AgriculturalHoldings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NationalId",
                table: "AgriculturalHoldings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AgriculturalHoldings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_AgriculturalHoldings_SourceOfOwnership_SourceOfOwnershipId",
                table: "AgriculturalHoldings",
                column: "SourceOfOwnershipId",
                principalTable: "SourceOfOwnership",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
