using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalArchitecture.Architecture.Migrations
{
    /// <inheritdoc />
    public partial class change_user_table_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Tag_UserId",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                schema: "auth",
                table: "Tag");

            migrationBuilder.RenameTable(
                name: "Tag",
                schema: "auth",
                newName: "User",
                newSchema: "auth");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                schema: "auth",
                table: "User",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_User_UserId",
                table: "Roles",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "User",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_User_UserId",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                schema: "auth",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                schema: "auth",
                newName: "Tag",
                newSchema: "auth");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                schema: "auth",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Tag_UserId",
                table: "Roles",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "Tag",
                principalColumn: "Id");
        }
    }
}
