using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests.Artistas;
using ScreenSound.Banco;
using ScreenSound.Modelos;

namespace ScreenSound.API.Endpoints
{
  public static class ArtistasExtensions
  {
    public static void AddEndPointsArtistas(this WebApplication app)
    {
      app.MapGet("/Artistas", ([FromServices] ArtistaDAL artistaDAL) =>
      {
        return Results.Ok(artistaDAL.Listar());
      });

      app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> artistaDAL, string nome) =>
      {
        var artista = artistaDAL.RecuperarPor(x => x.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        if (artista is null)
          return Results.NotFound();

        return Results.Ok(artista);
      });

      app.MapPost("/Artistas", ([FromServices] ArtistaDAL artistaDAL, [FromBody] ArtistaRequest artistaRequest) =>
      {
        var artista = new Artista(artistaRequest.Nome, artistaRequest.Bio);
        artistaDAL.Adicionar(artista);
        return Results.Created();
      });

      app.MapPut("/Artistas/{id}", ([FromServices] ArtistaDAL artistaDAL, [FromBody] ArtistaRequestEdit artistaRequestEdit, int id) =>
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

  }
}
