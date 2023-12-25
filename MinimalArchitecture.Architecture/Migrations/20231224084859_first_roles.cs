using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalArchitecture.Architecture.Migrations
{
    /// <inheritdoc />
    public partial class first_roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rol_User_UserId",
                schema: "auth",
                table: "Rol");

            migrationBuilder.DropIndex(
                name: "IX_Rol_UserId",
                schema: "auth",
                table: "Rol");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "auth",
                table: "Rol");

            migrationBuilder.CreateTable(
                name: "RolUser",
                schema: "auth",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RolUser_Rol_RolesId",
                        column: x => x.RolesId,
                        principalSchema: "auth",
                        principalTable: "Rol",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolUser_User_UsersId",
                        column: x => x.UsersId,
                        principalSchema: "auth",
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolUser_UsersId",
                schema: "auth",
                table: "RolUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolUser",
                schema: "auth");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "auth",
                table: "Rol",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rol_UserId",
                schema: "auth",
                table: "Rol",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rol_User_UserId",
                schema: "auth",
                table: "Rol",
                column: "UserId",
                principalSchema: "auth",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
