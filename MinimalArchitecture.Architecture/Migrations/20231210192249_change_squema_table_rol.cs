using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalArchitecture.Architecture.Migrations
{
    /// <inheritdoc />
    public partial class change_squema_table_rol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_User_UserId",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Rol",
                newSchema: "auth");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_UserId",
                schema: "auth",
                table: "Rol",
                newName: "IX_Rol_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rol",
                schema: "auth",
                table: "Rol",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rol_User_UserId",
                schema: "auth",
                table: "Rol",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rol_User_UserId",
                schema: "auth",
                table: "Rol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rol",
                schema: "auth",
                table: "Rol");

            migrationBuilder.RenameTable(
                name: "Rol",
                schema: "auth",
                newName: "Roles");

            migrationBuilder.RenameIndex(
                name: "IX_Rol_UserId",
                table: "Roles",
                newName: "IX_Roles_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_User_UserId",
                table: "Roles",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
