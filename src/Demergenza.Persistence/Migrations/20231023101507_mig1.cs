using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demergenza.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_tbl_admin_id",
                schema: "main",
                table: "tbl_admin");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "main",
                table: "tbl_admin");

            migrationBuilder.DropColumn(
                name: "MenuId",
                schema: "main",
                table: "tbl_admin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                schema: "main",
                table: "tbl_admin",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MenuId",
                schema: "main",
                table: "tbl_admin",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tbl_admin_id",
                schema: "main",
                table: "tbl_admin",
                column: "id",
                unique: true);
        }
    }
}
