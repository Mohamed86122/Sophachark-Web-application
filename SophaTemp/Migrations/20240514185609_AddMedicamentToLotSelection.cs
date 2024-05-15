using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SophaTemp.Migrations
{
    public partial class AddMedicamentToLotSelection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SelectedLotsJson",
                table: "Commandes",
                newName: "SelectedLotsString");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SelectedLotsString",
                table: "Commandes",
                newName: "SelectedLotsJson");
        }
    }
}
