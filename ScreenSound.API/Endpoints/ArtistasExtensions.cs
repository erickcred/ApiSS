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

      app.MapPost("/Artistas", async ([FromServices] IHostEnvironment env, [FromServices] DAL<Artista> artistaDAL, [FromBody] ArtistaRequest artistaRequest) =>
      {
        var nome = artistaRequest.Nome.Trim().Replace(" ", "_");
        var imagemArtista = $"{DateTime.Now.ToString("ddMMyyyhhss")}.{nome}.jpeg";

        var path = Path.Combine(env.ContentRootPath, "wwwroot", "FotosPerfil", imagemArtista);

        using MemoryStream ms = new MemoryStream(Convert.FromBase64String(artistaRequest.FotoPerfil!));
        using FileStream fs = new FileStream(path, FileMode.Create);
        await ms.CopyToAsync(fs);

        var artista = new Artista(artistaRequest.Nome, artistaRequest.Bio)
        {
          FotoPerfil = $"FotosPerfil/{imagemArtista}"
        };
        artistaDAL.Adicionar(artista);
        return Results.Created();
      });

      app.MapPut("/Artistas/{id}", async ([FromServices] IHostEnvironment env, [FromServices] DAL<Artista> artistaDAL, [FromBody] ArtistaRequestEdit artistaRequestEdit, int id) =>
      {
        var artista = artistaDAL.RecuperarPor(x => x.Id == id);
        if (artista is null)
          return Results.NotFound();

        var nome = artistaRequestEdit.Nome.Trim().Replace(" ", "_");
        var imagemArtista = $"{DateTime.Now.ToString("ddMMyyyhhss")}.{nome}.jpeg";
        if (artistaRequestEdit.FotoPerfil is not null)
        {
          var imagemAntiga = artista.FotoPerfil;
          var existe = File.Exists(imagemAntiga.ToString());
          if (existe)
          {
            File.Delete(imagemAntiga);
          }

          var path = Path.Combine(env.ContentRootPath, "wwwroot", "FotosPerfil", imagemArtista);

          using MemoryStream ms = new MemoryStream(Convert.FromBase64String(artistaRequestEdit.FotoPerfil!));
          using FileStream fs = new FileStream(path, FileMode.Create);
          await ms.CopyToAsync(fs);
          
          artista.FotoPerfil = $"FotosPerfil/{imagemArtista}" ?? artista.FotoPerfil;
        }

        artista.Nome = artistaRequestEdit.Nome;
        artista.Bio = artistaRequestEdit.Bio;

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
