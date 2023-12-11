using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalArchitecture.Architecture.Migrations
{
    /// <inheritdoc />
    public partial class table_roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rol_Tag_UserId",
                table: "Rol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rol",
                table: "Rol");

            migrationBuilder.RenameTable(
                name: "Rol",
                newName: "Roles");

            migrationBuilder.RenameIndex(
                name: "IX_Rol_UserId",
                table: "Roles",
                newName: "IX_Roles_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Roles",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Tag_UserId",
                table: "Roles",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Tag",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Tag_UserId",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Rol");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_UserId",
                table: "Rol",
                newName: "IX_Rol_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Rol",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rol",
                table: "Rol",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rol_Tag_UserId",
                table: "Rol",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Tag",
                principalColumn: "Id");
        }
    }
}
