using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demergenza.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image-path",
                schema: "main",
                table: "tbl_about_us",
                newName: "image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "image",
                schema: "main",
                table: "tbl_about_us",
                newName: "image-path");
        }
    }
}
