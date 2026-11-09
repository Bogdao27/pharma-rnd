using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaRnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "batch",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    manufactured_on = table.Column<DateOnly>(type: "date", nullable: false),
                    batch_number = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_batch", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "batch_components",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    batch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    lot_id = table.Column<Guid>(type: "uuid", nullable: false),
                    mass_in_grams = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_batch_components", x => x.id);
                    table.ForeignKey(
                        name: "fk_batch_components_batch_batch_id",
                        column: x => x.batch_id,
                        principalTable: "batch",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_batch_components_batch_id",
                table: "batch_components",
                column: "batch_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "batch_components");

            migrationBuilder.DropTable(
                name: "batch");
        }
    }
}
