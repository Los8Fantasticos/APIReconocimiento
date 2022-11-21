using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinimalAPI_Reconocimiento.Migrations
{
    public partial class updateTraficoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "bigint",
                table: "Trafico",
                newName: "PatentesReconocidas");

            migrationBuilder.AddColumn<long>(
                name: "PatentesNoReconocidas",
                table: "Trafico",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PatentesNoReconocidas",
                table: "Trafico");

            migrationBuilder.RenameColumn(
                name: "PatentesReconocidas",
                table: "Trafico",
                newName: "bigint");
        }
    }
}
