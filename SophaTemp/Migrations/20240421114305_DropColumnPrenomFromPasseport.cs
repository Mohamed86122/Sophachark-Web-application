using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class DropColumnPrenomFromPasseport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prenom",
                table: "Passeports");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Prenom",
                table: "Passeports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
