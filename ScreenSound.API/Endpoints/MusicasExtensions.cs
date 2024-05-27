using Microsoft.AspNetCore.Mvc;
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
        return Results.Ok(musicaDAL.Listar());
      });

      app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> musicaDAL, string nome) =>
      {
        var musica = musicaDAL.RecuperarPor(x => x.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        if (musica is null)
          return Results.NotFound();

        return Results.Ok(musica);
      });

      app.MapPost("/Musicas", ([FromServices] MusicaDAL musicaDAL, [FromBody] Musica model) =>
      {
        musicaDAL.Adicionar(model);
        return Results.Created();
      });

      app.MapPut("/Musicas/{id}", ([FromServices] MusicaDAL musicaDAL, [FromBody] Musica model, int id) =>
      {
        var musica = musicaDAL.RecuperarPor(x => x.Id == id);
        if (musica is null)
          return Results.NotFound();

        musicaDAL.Atualizar(model, id);
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
  }
}
