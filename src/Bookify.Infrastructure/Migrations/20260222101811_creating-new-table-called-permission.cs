using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bookify.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class creatingnewtablecalledpermission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_permission_role_role_id",
                table: "permission");

            migrationBuilder.DropPrimaryKey(
                name: "pk_permission",
                table: "permission");

            migrationBuilder.RenameTable(
                name: "permission",
                newName: "permissions");

            migrationBuilder.RenameIndex(
                name: "ix_permission_role_id",
                table: "permissions",
                newName: "ix_permissions_role_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_permissions",
                table: "permissions",
                column: "id");

            migrationBuilder.CreateTable(
                name: "role_permissions",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    permission_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permissions", x => new { x.role_id, x.permission_id });
                });

            migrationBuilder.InsertData(
                table: "permissions",
                columns: new[] { "id", "name", "role_id" },
                values: new object[] { 1, "users:read", null });

            migrationBuilder.InsertData(
                table: "role_permissions",
                columns: new[] { "permission_id", "role_id" },
                values: new object[] { 1, 1 });

            migrationBuilder.AddForeignKey(
                name: "fk_permissions_role_role_id",
                table: "permissions",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_permissions_role_role_id",
                table: "permissions");

            migrationBuilder.DropTable(
                name: "role_permissions");

            migrationBuilder.DropPrimaryKey(
                name: "pk_permissions",
                table: "permissions");

            migrationBuilder.DeleteData(
                table: "permissions",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.RenameTable(
                name: "permissions",
                newName: "permission");

            migrationBuilder.RenameIndex(
                name: "ix_permissions_role_id",
                table: "permission",
                newName: "ix_permission_role_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_permission",
                table: "permission",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_permission_role_role_id",
                table: "permission",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id");
        }
    }
}
