using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class RelacionandoMusicaGeneroEDiscografia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscografiaMusica",
                columns: table => new
                {
                    DiscografiasId = table.Column<int>(type: "int", nullable: false),
                    MusicasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscografiaMusica", x => new { x.DiscografiasId, x.MusicasId });
                    table.ForeignKey(
                        name: "FK_DiscografiaMusica_Discografia_DiscografiasId",
                        column: x => x.DiscografiasId,
                        principalTable: "Discografia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscografiaMusica_Musica_MusicasId",
                        column: x => x.MusicasId,
                        principalTable: "Musica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GeneroMusica",
                columns: table => new
                {
                    GenerosId = table.Column<int>(type: "int", nullable: false),
                    MusicasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneroMusica", x => new { x.GenerosId, x.MusicasId });
                    table.ForeignKey(
                        name: "FK_GeneroMusica_Genero_GenerosId",
                        column: x => x.GenerosId,
                        principalTable: "Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneroMusica_Musica_MusicasId",
                        column: x => x.MusicasId,
                        principalTable: "Musica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscografiaMusica_MusicasId",
                table: "DiscografiaMusica",
                column: "MusicasId");

            migrationBuilder.CreateIndex(
                name: "IX_GeneroMusica_MusicasId",
                table: "GeneroMusica",
                column: "MusicasId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscografiaMusica");

            migrationBuilder.DropTable(
                name: "GeneroMusica");
        }
    }
}
