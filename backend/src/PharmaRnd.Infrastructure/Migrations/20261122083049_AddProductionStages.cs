using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaRnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductionStages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_batch_component_batches_batch_id",
                table: "batch_component");

            migrationBuilder.DropPrimaryKey(
                name: "pk_batch_component",
                table: "batch_component");

            migrationBuilder.RenameTable(
                name: "batch_component",
                newName: "batch_components");

            migrationBuilder.RenameIndex(
                name: "ix_batch_component_batch_id",
                table: "batch_components",
                newName: "ix_batch_components_batch_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_batch_components",
                table: "batch_components",
                column: "id");

            migrationBuilder.CreateTable(
                name: "production_stages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    executed_at = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    batch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    stage_description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_production_stages", x => x.id);
                    table.ForeignKey(
                        name: "fk_production_stages_batches_batch_id",
                        column: x => x.batch_id,
                        principalTable: "batches",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_production_stages_batch_id",
                table: "production_stages",
                column: "batch_id");

            migrationBuilder.AddForeignKey(
                name: "fk_batch_components_batches_batch_id",
                table: "batch_components",
                column: "batch_id",
                principalTable: "batches",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_batch_components_batches_batch_id",
                table: "batch_components");

            migrationBuilder.DropTable(
                name: "production_stages");

            migrationBuilder.DropPrimaryKey(
                name: "pk_batch_components",
                table: "batch_components");

            migrationBuilder.RenameTable(
                name: "batch_components",
                newName: "batch_component");

            migrationBuilder.RenameIndex(
                name: "ix_batch_components_batch_id",
                table: "batch_component",
                newName: "ix_batch_component_batch_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_batch_component",
                table: "batch_component",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_batch_component_batches_batch_id",
                table: "batch_component",
                column: "batch_id",
                principalTable: "batches",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
