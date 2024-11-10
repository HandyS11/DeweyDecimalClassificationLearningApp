using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeweyDecimalClassification.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeweyEntries",
                columns: table => new
                {
                    Id = table.Column<float>(type: "REAL", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    ParentId = table.Column<float>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeweyEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeweyEntries_DeweyEntries_ParentId",
                        column: x => x.ParentId,
                        principalTable: "DeweyEntries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeweyEntries_ParentId",
                table: "DeweyEntries",
                column: "ParentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeweyEntries");
        }
    }
}
