using Microsoft.AspNetCore.Mvc;
using ScreenSound.API.Response.Musica.Generos;
using ScreenSound.Banco;
using ScreenSound.Shared.Modelos.Modelos;

namespace ScreenSound.API.Endpoints;

public static class GeneroExtensions
{
  public static void AddEndpointGeneros(this WebApplication app)
  {
    var groupBuilder = app.MapGroup("Generos")
      .RequireAuthorization()
      .WithTags("Generos");

    groupBuilder.MapGet("", ([FromServices] DAL<Genero> dal) =>
    {
      var generos = GenerosResponseConverter(dal.Listar().ToList());
      return generos;
    });

    groupBuilder.MapGet("{nome}", ([FromServices] DAL<Genero> dal, string nome) =>
    {
      var genero = dal.RecuperarPor(g => g.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
      if (genero is null) return Results.NotFound();

      var generoResponse = EntityToResponse(genero);
      return Results.Ok(generoResponse);
    });

    groupBuilder.MapPost("", ([FromServices] DAL<Genero> dal, [FromBody] GeneroResponse generoResponse) =>
    {
      var genero = new Genero(generoResponse.Nome)
      {
        Descricao = string.IsNullOrWhiteSpace(generoResponse.Descricao) ? "" : generoResponse.Descricao
      };

      dal.Adicionar(genero);
      return Results.NoContent();
    });

    groupBuilder.MapPut("{id}", ([FromServices] DAL<Genero> dal, [FromBody] GeneroResponse generoResponse, int id) =>
    {
      var genero = dal.RecuperarPor(g => g.Id == id);
      if (genero is null) return Results.NotFound();

      genero.Nome = generoResponse.Nome;
      genero.Descricao = string.IsNullOrWhiteSpace(generoResponse.Descricao) ? "" : generoResponse.Descricao;

      dal.Atualizar(genero, id);
      return Results.NoContent();
    });

    groupBuilder.MapDelete("{id}", ([FromServices] DAL<Genero> dal, int id) =>
    {
      var genero = dal.RecuperarPor(g => g.Id == id);
      if (genero is null) return Results.NotFound();

      dal.Deletar(genero);
      return Results.NoContent();
    });
  }

  private static ICollection<GeneroResponse> GenerosResponseConverter(ICollection<Genero> generos)
  {
    return generos.Select(g => EntityToResponse(g)).ToList();
  }

  private static GeneroResponse EntityToResponse(Genero genero)
  {
    return new GeneroResponse(genero.Id, genero.Nome, genero.Descricao);
  }
}
