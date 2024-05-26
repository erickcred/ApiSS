using ScreenSound.Banco;

namespace ScreenSound.Menus;

internal class MenuMostrarArtistas : Menu
{
  public override void Executar(ArtistaDAL artistaDAL)
  {
    base.Executar(artistaDAL);
    ExibirTituloDaOpcao("Exibindo todos os artistas registradas na nossa aplicação");

    foreach (var artista in artistaDAL.ListarArtistas())
      Console.WriteLine($"Artista: {artista}\r\n");

    Console.WriteLine("\nDigite uma tecla para voltar ao menu principal");
    Console.ReadKey();
    Console.Clear();
  }
}
