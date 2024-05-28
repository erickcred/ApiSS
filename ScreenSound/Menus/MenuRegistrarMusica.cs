﻿using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.Menus;

internal class MenuRegistrarMusica : Menu
{
  public override void Executar(DAL<Artista> artistaDAL)
  {
    base.Executar(artistaDAL);
    ExibirTituloDaOpcao("Registro de músicas");

    Console.Write("Digite o artista cuja música deseja registrar: ");
    string nomeDoArtista = Console.ReadLine()!;

    var artistaRecuperado = artistaDAL
      .RecuperarPor(x => x.Nome.ToLower().Equals(nomeDoArtista.ToLower()));
    if (artistaRecuperado is not null)
    {
      Console.Write("Agora digite o título da música: ");
      string tituloDaMusica = Console.ReadLine()!;

      Console.Write("Agora digite o ano de lançamento da música exp: (1990): ");
      string anoDeLancamento = Console.ReadLine()!;
      artistaRecuperado.AdicionarMusica(new Musica(tituloDaMusica) { AnoLancamento = Convert.ToInt32(anoDeLancamento)});

      Console.WriteLine($"A música {tituloDaMusica} de {nomeDoArtista} foi registrada com sucesso!");
      artistaDAL.Atualizar(artistaRecuperado, artistaRecuperado.Id);

      Thread.Sleep(4000);
      Console.Clear();
    }
    else
    {
      Console.WriteLine($"\nO artista {nomeDoArtista} não foi encontrado!");
      Console.WriteLine("Digite uma tecla para voltar ao menu principal");
      Console.ReadKey();
      Console.Clear();
    }
  }
}
