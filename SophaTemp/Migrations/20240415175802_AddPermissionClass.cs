using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class AddPermissionClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PermissionsJson",
                table: "Passeports");

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasseportId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.PermissionId);
                    table.ForeignKey(
                        name: "FK_permissions_Passeports_PasseportId",
                        column: x => x.PasseportId,
                        principalTable: "Passeports",
                        principalColumn: "PasseportId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_permissions_PasseportId",
                table: "permissions",
                column: "PasseportId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.AddColumn<string>(
                name: "PermissionsJson",
                table: "Passeports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
