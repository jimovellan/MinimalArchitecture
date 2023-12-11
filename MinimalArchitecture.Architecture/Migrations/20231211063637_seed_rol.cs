using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalArchitecture.Architecture.Migrations
{
    /// <inheritdoc />
    public partial class seed_rol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RolType",
                schema: "auth",
                table: "User");

            migrationBuilder.InsertData(
                schema: "auth",
                table: "Rol",
                columns: new[] { "Id", "Description", "RolType", "UserId" },
                values: new object[] { 1, "Administrador", 515, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "auth",
                table: "Rol",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "RolType",
                schema: "auth",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
