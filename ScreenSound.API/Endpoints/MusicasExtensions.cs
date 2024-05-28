using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests.Musicas;
using ScreenSound.API.Response.Musica;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtensions
  {
    public static void AddEndpointMusicas(this WebApplication app)
    {
      app.MapGet("/Musicas", ([FromServices] MusicaDAL musicaDAL) =>
      {
        var musicaResponse = ParaListaMusicasResponse(musicaDAL.Listar());
        return Results.Ok(musicaResponse);
      });

      app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> musicaDAL, string nome) =>
      {
        var musica = musicaDAL.RecuperarPor(x => x.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        if (musica is null)
          return Results.NotFound();

        var musicaResponse = ParaMusicaResponse(musica);
        return Results.Ok(musicaResponse);
      });

      app.MapPost("/Musicas", ([FromServices] DAL<Musica> musicaDAL, [FromBody] MusicaRequest musicaRequest) =>
      {
        var musica = new Musica(musicaRequest.Nome);
        if (musicaRequest.AnoLancamento != null)
          musica.AnoLancamento = (int)musicaRequest.AnoLancamento;
        musicaDAL.Adicionar(musica);
        return Results.Created();
      });

      app.MapPut("/Musicas/{id}", ([FromServices] DAL<Musica> musicaDAL, [FromBody] MusicaRequestEdit musicaRequestEdit, int id) =>
      {
        var musica = musicaDAL.RecuperarPor(x => x.Id == id);
        if (musica is null)
          return Results.NotFound();

        musica.Nome = musicaRequestEdit.Nome ?? musica.Nome;
        musica.AnoLancamento = musicaRequestEdit.AnoLancamento ?? musica.AnoLancamento;
        musica.Artista = musicaRequestEdit.Artista ?? musica.Artista;

        musicaDAL.Atualizar(musica, id);
        return Results.Ok();
      });

      app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> musicaDAL, int id) =>
      {
        var musica = musicaDAL.RecuperarPor(x => x.Id == id);
        if (musica is null)
          return Results.NotFound();

        musicaDAL.Deletar(musica);
        return Results.NoContent();
      });
    }

    private static ICollection<MusicaResponse> ParaListaMusicasResponse(IEnumerable<Musica> musicas)
    {
      return musicas.Select(a => ParaMusicaResponse(a)).ToList();
    }

    private static MusicaResponse ParaMusicaResponse(Musica musica)
    {
      return new MusicaResponse(musica.Id, musica.Nome, musica.Artista?.Id, musica.Artista?.Nome);
    }
  }
}
