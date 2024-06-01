using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests.Musicas;
using ScreenSound.API.Requests.Musicas.Discografias;
using ScreenSound.API.Requests.Musicas.Generos;
using ScreenSound.API.Response.Musica;
using ScreenSound.API.Response.Musica.Discografias;
using ScreenSound.API.Response.Musica.Generos;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Endpoints
{
    public static class MusicasExtensions
  {
    public static void AddEndpointMusicas(this WebApplication app)
    {
      app.MapGet("/Musicas", async ([FromServices] DAL<Musica> musicaDAL) =>
      {
        var musicas = musicaDAL.Listar();
        if (musicas is null) return Results.NotFound();

        var musicasResponse = MusicasResponseConverter(musicas.ToList());
        return Results.Ok(musicasResponse);
      });

      app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> musicaDAL, string nome) =>
      {
        var musica = musicaDAL.RecuperarPor(x => x.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        if (musica is null)
          return Results.NotFound();

        var musicaResponse = EntityToResponse(musica);
        return Results.Ok(musicaResponse);
      });

      app.MapPost("/Musicas", (
        [FromServices] DAL<Musica> musicaDAL,
        [FromServices] DAL<Genero> generoDAL,
        [FromServices] DAL<Discografia> disografiaDAL,
        [FromBody] MusicaRequest musicaRequest) =>
      {
        var musica = new Musica(musicaRequest.Nome.Trim())
        {
          ArtistaId = musicaRequest.ArtistaId,
          AnoLancamento = musicaRequest.AnoLancamento,
          Generos = musicaRequest.Generos is not null ?
            GeneroRequestConverter(musicaRequest.Generos, generoDAL) : new List<Genero>(),
          Discografias = musicaRequest.Discografias is not null ?
            DiscografiaRequestConverter(musicaRequest.Discografias, disografiaDAL) : new List<Discografia>()
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
        musica.AnoLancamento = musicaRequestEdit.AnoLancamento;

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

    #region RequestConverter

    #region Discografia
    private static ICollection<Discografia> DiscografiaRequestConverter(ICollection<DiscografiaRequest> discografias, DAL<Discografia> discografiaDAL)
    {
      var listaDeDiscografias = new List<Discografia>();
      foreach (var item in discografias)
      {
        var entity = RequestToEntity(item);
        var discografia = discografiaDAL.RecuperarPor(d => d.Nome.Equals(entity.Nome, StringComparison.OrdinalIgnoreCase));
        
        if (discografia is not null)
          listaDeDiscografias.Add(discografia);
        else
          listaDeDiscografias.Add(entity);
      }
      return listaDeDiscografias;
    }
    
    private static Discografia RequestToEntity(DiscografiaRequest discografia)
    {
      return new Discografia(discografia.Nome) { Descricao = discografia.Descricao };
    }
    #endregion

    #region Generos
    private static ICollection<Genero> GeneroRequestConverter(ICollection<GeneroRequest> generos, DAL<Genero> generoDAL)
    {
      var listaDeGeneros = new List<Genero>();
      foreach (var item in generos)
      {
        var entity = RequestToEntity(item);
        var genero = generoDAL.RecuperarPor(g => g.Nome.Equals(item.Nome, StringComparison.OrdinalIgnoreCase));
        
        if (genero is not null)
          listaDeGeneros.Add(genero);
        else
          listaDeGeneros.Add(entity);
      }
      return listaDeGeneros;
    }

    private static Genero RequestToEntity(GeneroRequest genero)
    {
      return new Genero(genero.Nome) { Descricao = genero.Descricao };
    }
    #endregion

    #endregion



    #region Response Converter
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
      return new MusicaResponse(musica.Id, musica.Nome!, musica.Artista!.Id, musica.Artista.Nome, musica.AnoLancamento);
    }

    private static GeneroResponse EntityToResponse(Genero genero)
    {
      return new GeneroResponse(genero.Id, genero.Nome, genero.Descricao!);
    }

    private static DiscografiaResponse EntityToResponse(Discografia discografia)
    {
      return new DiscografiaResponse(discografia.Id, discografia.Nome, discografia.Descricao);
    }
    #endregion
  }
}
