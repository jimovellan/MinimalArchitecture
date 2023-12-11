using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalArchitecture.Architecture.Migrations
{
    /// <inheritdoc />
    public partial class rol_type : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RolType",
                schema: "auth",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RolType",
                schema: "auth",
                table: "User");
        }
    }
}
