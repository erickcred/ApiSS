using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Requests.Musicas.Discografias;
using ScreenSound.API.Response.Musica.Discografias;
using ScreenSound.Banco;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Endpoints;

public static class DiscografiaExtensions
{
  public static void AddEndpointDiscografia(this WebApplication app)
  {
    app.MapGet("/Discografias", ([FromServices] DAL<Discografia> dal) =>
    {
      var discografias = DiscografiasResponseConverter(dal.Listar().ToList());
      return Results.Ok(discografias);
    });

    app.MapGet("/Discografias/{nome}", ([FromServices] DAL<Discografia> dal, string nome) =>
    {
      var discografia = dal.RecuperarPor(d => d.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
      if (discografia is null)
        return Results.NotFound();

      var discografiaResponse = EntityToResponse(discografia);
      return Results.Ok(discografiaResponse);
    });

    app.MapPost("/Discografias", ([FromServices] DAL<Discografia> dal, [FromBody] DiscografiaRequest discografiaRequest) =>
    {
      var discografia = new Discografia(discografiaRequest.Nome)
      {
        Descricao = string.IsNullOrWhiteSpace(discografiaRequest.Descricao) ? "" : discografiaRequest.Descricao
      };
      dal.Adicionar(discografia);

      return Results.Created();
    });

    app.MapPut("/Discografias/{id}", ([FromServices] DAL<Discografia> dal, [FromBody] DiscografiaRequest discografiaRequest, int id) =>
    {
      var discografia = dal.RecuperarPor(d => d.Id == id);
      if (discografia is null) return Results.NotFound();

      discografia.Nome = discografiaRequest.Nome;
      discografia.Descricao = string.IsNullOrWhiteSpace(discografiaRequest.Descricao) ? "" : discografiaRequest.Descricao;
      

      dal.Atualizar(discografia, id);
      return Results.NoContent();
    });

    app.MapDelete("/Discografias/{id}", ([FromServices] DAL<Discografia> dal, int id) =>
    {
      var discografia = dal.RecuperarPor(d => d.Id == id);
      if (discografia is null) return Results.NotFound();

      dal.Deletar(discografia);
      return Results.NoContent();

    });
  }

  #region Response
  private static ICollection<DiscografiaResponse> DiscografiasResponseConverter(ICollection<Discografia> discografias)
  {
    return discografias.Select(d => EntityToResponse(d)).ToList();
  }

  private static DiscografiaResponse EntityToResponse(Discografia discografia)
  {
    return new DiscografiaResponse(discografia.Id, discografia.Nome, discografia.Descricao);
  }
  #endregion


  
}
