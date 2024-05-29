using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests.Musicas;
using ScreenSound.API.Response.Musica;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtensions
  {
    public static void AddEndpointMusicas(this WebApplication app)
    {
      app.MapGet("/Musicas", ([FromServices] DAL<Musica> musicaDAL) =>
      {
        var musicaResponse = MusicasResponseConverter(musicaDAL.Listar().ToList());
        return Results.Ok(musicaResponse);
      });

      app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> musicaDAL, string nome) =>
      {
        var musica = musicaDAL.RecuperarPor(x => x.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        if (musica is null)
          return Results.NotFound();

        var musicaResponse = EntityToResponse(musica);
        return Results.Ok(musicaResponse);
      });

      app.MapPost("/Musicas", ([FromServices] DAL<Musica> musicaDAL, [FromBody] MusicaRequest musicaRequest) =>
      {
        var musica = new Musica(musicaRequest.Nome)
        {
          ArtistaId = musicaRequest.ArtistaId,
          AnoLancamento = musicaRequest.AnoLancamento,
          Generos = musicaRequest.Generos is not null ?
            GeneroRequestConverter(musicaRequest.Generos) : new List<Genero>(),
          Discografias = musicaRequest.Discografias is not null ?
            DiscografiaRequestConverter(musicaRequest.Discografias) : new List<Discografia>()
        };
        
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
        musica.Generos = musicaRequestEdit.Generos is not null ?
            GeneroRequestEditConverter(musicaRequestEdit.Generos) : new List<Genero>();
        musica.Discografias = musicaRequestEdit.Discografias is not null ?
            DiscografiaRequestEditConverter(musicaRequestEdit.Discografias) : new List<Discografia>();

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

    #region Request
    private static ICollection<Discografia> DiscografiaRequestConverter(ICollection<DiscografiaRequest> discografias)
    {
      return discografias.Select(a => RequestToEntity(a)).ToList();
    }

    private static Discografia RequestToEntity(DiscografiaRequest discografia)
    {
      return new Discografia() { Nome = discografia.Nome, Descricao = discografia.Descricao };
    }

    private static ICollection<Discografia> DiscografiaRequestEditConverter(ICollection<DiscografiaRequestEdit> discografias)
    {
      return discografias.Select(a => RequestToEntity(a)).ToList();
    }

    private static Discografia RequestToEntity(DiscografiaRequestEdit discografia)
    {
      return new Discografia() { Id = discografia.Id, Nome = discografia.Nome, Descricao = discografia.Descricao };
    }

    private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos)
    {
      return generos.Select(a => RequestToEntity(a)).ToList();
    }

    private static Genero RequestToEntity(GeneroRequest genero)
    {
      return new Genero() { Nome = genero.Nome, Descricao = genero.Descricao };
    }

    private static ICollection<Genero> GeneroRequestEditConverter(ICollection<GeneroRequestEdit> generos)
    {
      return generos.Select(a => RequestToEntity(a)).ToList();
    }

    private static Genero RequestToEntity(GeneroRequestEdit genero)
    {
      return new Genero() {Id = genero.Id, Nome = genero.Nome, Descricao = genero.Descricao };
    }

    #endregion



    #region Response
    private static ICollection<MusicaResponse> MusicasResponseConverter(ICollection<Musica> musicas)
    {
      return musicas.Select(a => EntityToResponse(a)).ToList();
    }

    private static ICollection<GeneroResponse> GenerosResponseConverter(ICollection<Genero> generos)
    {
      return generos.Select(g => EntityToResponse(g)).ToList();
    }

    private static ICollection<DiscografiaResponse> DiscografiasResponseConverter(ICollection<Discografia> discogafrias)
    {
      return discogafrias.Select(g => EntityToResponse(g)).ToList();
    }

    private static MusicaResponse EntityToResponse(Musica musica)
    {
      return new MusicaResponse(musica.Id, musica.Nome, musica?.ArtistaId, musica?.Artista?.Nome, 
        GenerosResponseConverter(musica.Generos), DiscografiasResponseConverter(musica.Discografias));
    }

    private static GeneroResponse EntityToResponse(Genero genero)
    {
      return new GeneroResponse(genero.Id, genero.Nome, genero.Descricao);
    }

    private static DiscografiaResponse EntityToResponse(Discografia discografia)
    {
      return new DiscografiaResponse(discografia.Id, discografia.Nome, discografia.Descricao);
    }


    #endregion
  }
}
