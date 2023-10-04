using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demergenza.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.CreateTable(
                name: "tbl_admin",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "varchar", maxLength: 24, nullable: false),
                    password = table.Column<string>(type: "varchar", maxLength: 32, nullable: false),
                    fullName = table.Column<string>(type: "varchar", maxLength: 48, nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    MenuId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_admin", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_category",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar", maxLength: 48, nullable: false),
                    AdminId = table.Column<Guid>(type: "uuid", nullable: false),
                    image = table.Column<string>(type: "varchar", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_category", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_category_tbl_admin_AdminId",
                        column: x => x.AdminId,
                        principalSchema: "main",
                        principalTable: "tbl_admin",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_menu",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "varchar", maxLength: 48, nullable: false),
                    Ingredients = table.Column<string>(type: "varchar", maxLength: 250, nullable: false),
                    image = table.Column<string>(type: "varchar", maxLength: 250, nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdminId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_menu", x => x.id);
                    table.ForeignKey(
                        name: "FK_tbl_menu_tbl_admin_AdminId",
                        column: x => x.AdminId,
                        principalSchema: "main",
                        principalTable: "tbl_admin",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_menu_tbl_category_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "main",
                        principalTable: "tbl_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_admin_id",
                schema: "main",
                table: "tbl_admin",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_category_AdminId",
                schema: "main",
                table: "tbl_category",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_menu_AdminId",
                schema: "main",
                table: "tbl_menu",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_menu_CategoryId",
                schema: "main",
                table: "tbl_menu",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_menu",
                schema: "main");

            migrationBuilder.DropTable(
                name: "tbl_category",
                schema: "main");

            migrationBuilder.DropTable(
                name: "tbl_admin",
                schema: "main");
        }
    }
}
