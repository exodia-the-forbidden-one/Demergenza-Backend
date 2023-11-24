using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demergenza.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                schema: "main",
                table: "tbl_menu",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                schema: "main",
                table: "tbl_category",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                schema: "main",
                table: "tbl_admin",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date",
                schema: "main",
                table: "tbl_menu");

            migrationBuilder.DropColumn(
                name: "date",
                schema: "main",
                table: "tbl_category");

            migrationBuilder.DropColumn(
                name: "date",
                schema: "main",
                table: "tbl_admin");
        }
    }
}
