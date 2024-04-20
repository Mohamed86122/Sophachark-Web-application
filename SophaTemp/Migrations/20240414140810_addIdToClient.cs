using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class addIdToClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Personnes",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Personnes");
        }
    }
}
