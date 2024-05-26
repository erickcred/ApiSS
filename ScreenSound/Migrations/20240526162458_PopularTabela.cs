using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
  /// <inheritdoc />
  public partial class PopularTabela : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.InsertData("Artista", new string[] {"Nome", "Bio", "FotoPerfil"}, new object[] {"Djavan", "Djavan Caetano Viana é um cantor, compositor, arranjador, produtor musical, empresário, violonista e ex-fotebolista brasileiro.", "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png" });
      migrationBuilder.InsertData("Artista", new string[] {"Nome", "Bio", "FotoPerfil"}, new object[] {"Mamonas Assasinas", "Mamonas Assassinas, anteriormente chamada de Utopia, foi uma banda brasileira de rock cômico formada em Guarulhos em 1990.", "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png" });
      migrationBuilder.InsertData("Artista", new string[] {"Nome", "Bio", "FotoPerfil"}, new object[] {"Sandy & Junior", "Sandy & Junior foi uma dupla vocal brasileira formada pelos irmãos Sandy (n. 1983) e Junior Lima (n. 1984).", "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.Sql("delete from artista");
    }
  }
}
