using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaRnd.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteFlags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "products",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "batches",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                table: "batch_component",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "products");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "batches");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                table: "batch_component");
        }
    }
}
