using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arch.Migrations
{
    /// <inheritdoc />
    public partial class status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "AgriculturalHoldings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReviewed",
                table: "AgriculturalHoldings",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReceivingRequest",
                table: "AgriculturalHoldings",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "AgriculturalHoldings");

            migrationBuilder.DropColumn(
                name: "IsReviewed",
                table: "AgriculturalHoldings");

            migrationBuilder.DropColumn(
                name: "ReceivingRequest",
                table: "AgriculturalHoldings");
        }
    }
}
