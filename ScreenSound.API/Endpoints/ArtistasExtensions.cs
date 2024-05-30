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
        var artistas = artistaDAL.Listar();
        if (artistas is null) return Results.NotFound();

        var artistasResponse = ParaListaArtistaResponse(artistas.ToList());
        return Results.Ok(artistasResponse);
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

    #region Response Converter
    private static ICollection<ArtistaResponse> ParaListaArtistaResponse(ICollection<Artista> artistas)
    {
      return (ICollection<ArtistaResponse>)artistas.Select(a => ParaArtistaResponse(a)).ToList();
    }

    private static ArtistaResponse ParaArtistaResponse(Artista artista)
    {
      return new ArtistaResponse(artista.Id, artista.Nome, artista.Bio, artista.FotoPerfil);
    }
    #endregion

  }
}
