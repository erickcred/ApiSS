using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests.Artistas;
using ScreenSound.API.Response.Artista;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
  public static class ArtistasExtensions
  {
    public static void AddEndPointsArtistas(this WebApplication app)
    {
      app.MapGet("/Artistas", ([FromServices] DAL<Artista> artistaDAL) =>
      {
        var artistaResponse = ParaListaArtistaResponse(artistaDAL.Listar());
        return Results.Ok(artistaResponse);
      });

      app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> artistaDAL, string nome) =>
      {
        var artista = artistaDAL.RecuperarPor(x => x.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        if (artista is null)
          return Results.NotFound();

        var artistaResponse = ParaArtistaResponse(artista);
        return Results.Ok(artistaResponse);
      });

      app.MapPost("/Artistas", ([FromServices] DAL<Artista> artistaDAL, [FromBody] ArtistaRequest artistaRequest) =>
      {
        var artista = new Artista(artistaRequest.Nome, artistaRequest.Bio);
        artistaDAL.Adicionar(artista);
        return Results.Created();
      });

      app.MapPut("/Artistas/{id}", ([FromServices] DAL<Artista> artistaDAL, [FromBody] ArtistaRequestEdit artistaRequestEdit, int id) =>
      {
        var artista = artistaDAL.RecuperarPor(x => x.Id == id);
        if (artista is null)
          return Results.NotFound();

        artista.Nome = artistaRequestEdit.Nome;
        artista.Bio = artistaRequestEdit.Bio;
        artista.FotoPerfil = artistaRequestEdit.FotoPerfil ?? artista.FotoPerfil;

        foreach (var musica in artista.Musicas)
        {
          var musicaParaAtualizar = artista.Musicas.FirstOrDefault(x => x.Id == artistaRequestEdit.Musica.Id);
          if (musicaParaAtualizar is not null)
          {
            musicaParaAtualizar.Nome = artistaRequestEdit.Musica.Nome;
            musicaParaAtualizar.AnoLancamento = artistaRequestEdit.Musica.AnoLancamento;
            break;
          }
          else
            artista.AdicionarMusica(artistaRequestEdit.Musica);
        }

        artistaDAL.Atualizar(artista, id);
        return Results.Ok();
      });

      app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> artistaDAL, int id) =>
      {
        var artista = artistaDAL.RecuperarPor(x => x.Id == id);
        if (artista is null)
          return Results.NotFound();

        artistaDAL.Deletar(artista);
        return Results.NoContent();
      });
    }

    private static ICollection<ArtistaResponse> ParaListaArtistaResponse(IEnumerable<Artista> artistas)
    {
      return (ICollection<ArtistaResponse>)artistas.Select(a => ParaArtistaResponse(a)).ToList();
    }

    private static ArtistaResponse ParaArtistaResponse(Artista artista)
    {
      return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);
    }

  }
}
