using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarColunaAnoLancamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Musicas",
                table: "Musicas");

            migrationBuilder.RenameTable(
                name: "Musicas",
                newName: "Musica");

            migrationBuilder.RenameColumn(
                name: "ArtistaId",
                table: "Musica",
                newName: "AnoLancamento");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Musica",
                table: "Musica",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Musica",
                table: "Musica");

            migrationBuilder.RenameTable(
                name: "Musica",
                newName: "Musicas");

            migrationBuilder.RenameColumn(
                name: "AnoLancamento",
                table: "Musicas",
                newName: "ArtistaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Musicas",
                table: "Musicas",
                column: "Id");
        }
    }
}
