using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.Menus
{
  internal class MenuMostrarMusicasPorAnoDeLancamento : Menu
  {
    public override void Executar(ArtistaDAL artistaDAL)
    {
      base.Executar(artistaDAL);
      ExibirTituloDaOpcao("Exibir Musicas pelo ano lançamento");

      Console.Write("Digite o ano que deseja pesquisa: ");
      string ano = Console.ReadLine()!;

      var musicasDal = new DAL<Musica>(new ScreenSoundContext());
      var musicas = musicasDal.ListarPor(x => x.AnoLancamento == Convert.ToInt32(ano));

      if (musicas.Count() > 0)
      {
        Console.WriteLine($"\r\nMusicas lançadas em {ano}");
        foreach (var musica in musicas)
        {
          musica.ExibirFichaTecnica();
        }

        Console.WriteLine("\r\nDigite um tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
      }
      else
      {
        Console.WriteLine($"\r\nNão foram encontradas musica com esse ano de lançamento!");
        Console.WriteLine("\r\nDigite um tecla para voltar ao menu principal");
        Console.ReadKey();
        Console.Clear();
      }

    }
  }
}
