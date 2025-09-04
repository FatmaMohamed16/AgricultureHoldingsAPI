using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arch.Migrations
{
    /// <inheritdoc />
    public partial class Five : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Photos_AgriculturalHoldings_AgriculturalHoldingId",
                table: "Photos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Photos",
                table: "Photos");

            migrationBuilder.RenameTable(
                name: "Photos",
                newName: "Attachments");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Attachments",
                newName: "Attachment");

            migrationBuilder.RenameIndex(
                name: "IX_Photos_AgriculturalHoldingId",
                table: "Attachments",
                newName: "IX_Attachments_AgriculturalHoldingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_AgriculturalHoldings_AgriculturalHoldingId",
                table: "Attachments",
                column: "AgriculturalHoldingId",
                principalTable: "AgriculturalHoldings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_AgriculturalHoldings_AgriculturalHoldingId",
                table: "Attachments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attachments",
                table: "Attachments");

            migrationBuilder.RenameTable(
                name: "Attachments",
                newName: "Photos");

            migrationBuilder.RenameColumn(
                name: "Attachment",
                table: "Photos",
                newName: "Image");

            migrationBuilder.RenameIndex(
                name: "IX_Attachments_AgriculturalHoldingId",
                table: "Photos",
                newName: "IX_Photos_AgriculturalHoldingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Photos",
                table: "Photos",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Photos_AgriculturalHoldings_AgriculturalHoldingId",
                table: "Photos",
                column: "AgriculturalHoldingId",
                principalTable: "AgriculturalHoldings",
                principalColumn: "Id");
        }
    }
}
