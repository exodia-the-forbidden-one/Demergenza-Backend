using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demergenza.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "main",
                table: "tbl_menu",
                type: "varchar(48)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 48);

            migrationBuilder.AlterColumn<string>(
                name: "image",
                schema: "main",
                table: "tbl_menu",
                type: "varchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 250,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ingredients",
                schema: "main",
                table: "tbl_menu",
                type: "varchar(250)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "main",
                table: "tbl_category",
                type: "varchar(48)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 48);

            migrationBuilder.AlterColumn<string>(
                name: "image",
                schema: "main",
                table: "tbl_category",
                type: "varchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 250);

            migrationBuilder.AlterColumn<string>(
                name: "username",
                schema: "main",
                table: "tbl_admin",
                type: "varchar(24)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 24);

            migrationBuilder.AlterColumn<string>(
                name: "password",
                schema: "main",
                table: "tbl_admin",
                type: "varchar(32)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 32);

            migrationBuilder.AlterColumn<string>(
                name: "fullName",
                schema: "main",
                table: "tbl_admin",
                type: "varchar(48)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 48);

            migrationBuilder.CreateTable(
                name: "tbl_about_us",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    textcontent = table.Column<string>(name: "text-content", type: "varchar(2000)", nullable: true),
                    imagepath = table.Column<string>(name: "image-path", type: "varchar(150)", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_about_us", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_contact",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    address = table.Column<string>(type: "varchar(150)", nullable: false),
                    email = table.Column<string>(type: "varchar(60)", nullable: false),
                    phone = table.Column<string>(type: "varchar(24)", nullable: false),
                    secondphone = table.Column<string>(name: "second-phone", type: "varchar(24)", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_contact", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_hero",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    imagesrc = table.Column<string>(name: "image-src", type: "varchar(250)", nullable: false),
                    imagewidth = table.Column<string>(name: "image-width", type: "varchar(8)", nullable: true),
                    alt = table.Column<string>(type: "varchar(30)", nullable: true),
                    title = table.Column<string>(type: "varchar(40)", nullable: true),
                    titlesrc = table.Column<string>(name: "title-src", type: "varchar(250)", nullable: true),
                    subtitle = table.Column<string>(type: "varchar(600)", nullable: true),
                    top = table.Column<string>(type: "varchar(8)", nullable: true),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_hero", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_about_us",
                schema: "main");

            migrationBuilder.DropTable(
                name: "tbl_contact",
                schema: "main");

            migrationBuilder.DropTable(
                name: "tbl_hero",
                schema: "main");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "main",
                table: "tbl_menu",
                type: "varchar",
                maxLength: 48,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(48)");

            migrationBuilder.AlterColumn<string>(
                name: "image",
                schema: "main",
                table: "tbl_menu",
                type: "varchar",
                maxLength: 250,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Ingredients",
                schema: "main",
                table: "tbl_menu",
                type: "varchar",
                maxLength: 250,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                schema: "main",
                table: "tbl_category",
                type: "varchar",
                maxLength: 48,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(48)");

            migrationBuilder.AlterColumn<string>(
                name: "image",
                schema: "main",
                table: "tbl_category",
                type: "varchar",
                maxLength: 250,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                schema: "main",
                table: "tbl_admin",
                type: "varchar",
                maxLength: 24,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(24)");

            migrationBuilder.AlterColumn<string>(
                name: "password",
                schema: "main",
                table: "tbl_admin",
                type: "varchar",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(32)");

            migrationBuilder.AlterColumn<string>(
                name: "fullName",
                schema: "main",
                table: "tbl_admin",
                type: "varchar",
                maxLength: 48,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(48)");
        }
    }
}
