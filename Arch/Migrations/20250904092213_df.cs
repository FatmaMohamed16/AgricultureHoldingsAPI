using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arch.Migrations
{
    /// <inheritdoc />
    public partial class df : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Markazes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markazes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SourceOfOwnership",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceOfOwnership", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Madina_Maglas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarkazId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Madina_Maglas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Madina_Maglas_Markazes_MarkazId",
                        column: x => x.MarkazId,
                        principalTable: "Markazes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    MarkazId = table.Column<int>(type: "int", nullable: false),
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                    table.ForeignKey(
                        name: "FK_User_Markazes_MarkazId",
                        column: x => x.MarkazId,
                        principalTable: "Markazes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_User_Role_UserRoleId",
                        column: x => x.UserRoleId,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgriculturalHoldings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingsCount = table.Column<int>(type: "int", nullable: true),
                    HyazaNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarkazId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataResourses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActualArea = table.Column<double>(type: "float", nullable: true),
                    RegistedArea = table.Column<double>(type: "float", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SourceOfOwnershipId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgriculturalHoldings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgriculturalHoldings_Markazes_MarkazId",
                        column: x => x.MarkazId,
                        principalTable: "Markazes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgriculturalHoldings_SourceOfOwnership_SourceOfOwnershipId",
                        column: x => x.SourceOfOwnershipId,
                        principalTable: "SourceOfOwnership",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgriculturalHoldings_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AgriculturalHoldingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Photos_AgriculturalHoldings_AgriculturalHoldingId",
                        column: x => x.AgriculturalHoldingId,
                        principalTable: "AgriculturalHoldings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PropertyCoordinates",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    X = table.Column<double>(type: "float", nullable: false),
                    Y = table.Column<double>(type: "float", nullable: false),
                    AgriculturalHoldingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyCoordinates", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PropertyCoordinates_AgriculturalHoldings_AgriculturalHoldingId",
                        column: x => x.AgriculturalHoldingId,
                        principalTable: "AgriculturalHoldings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgriculturalHoldings_MarkazId",
                table: "AgriculturalHoldings",
                column: "MarkazId");

            migrationBuilder.CreateIndex(
                name: "IX_AgriculturalHoldings_SourceOfOwnershipId",
                table: "AgriculturalHoldings",
                column: "SourceOfOwnershipId");

            migrationBuilder.CreateIndex(
                name: "IX_AgriculturalHoldings_UserId",
                table: "AgriculturalHoldings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Madina_Maglas_MarkazId",
                table: "Madina_Maglas",
                column: "MarkazId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_AgriculturalHoldingId",
                table: "Photos",
                column: "AgriculturalHoldingId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyCoordinates_AgriculturalHoldingId",
                table: "PropertyCoordinates",
                column: "AgriculturalHoldingId");

            migrationBuilder.CreateIndex(
                name: "IX_User_MarkazId",
                table: "User",
                column: "MarkazId");

            migrationBuilder.CreateIndex(
                name: "IX_User_UserRoleId",
                table: "User",
                column: "UserRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Madina_Maglas");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "PropertyCoordinates");

            migrationBuilder.DropTable(
                name: "AgriculturalHoldings");

            migrationBuilder.DropTable(
                name: "SourceOfOwnership");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Markazes");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
