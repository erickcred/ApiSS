using Microsoft.AspNetCore.Mvc;
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

      app.MapPost("/Artistas", ([FromServices] ArtistaDAL artistaDAL, [FromBody] Artista model) =>
      {
        artistaDAL.Adicionar(model);
        return Results.Created();
      });

      app.MapPut("/Artistas/{id}", ([FromServices] ArtistaDAL artistaDAL, [FromBody] Artista model, int id) =>
      {
        var artista = artistaDAL.RecuperarPor(x => x.Id == id);
        if (artista is null)
          return Results.NotFound();

        artistaDAL.Atualizar(model, id);
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
