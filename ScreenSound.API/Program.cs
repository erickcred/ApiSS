using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<ArtistaDAL>();
builder.Services.AddTransient<MusicaDAL>();
builder.Services.AddTransient<DAL<Musica>>();

// Corrigir JsonException
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(
  options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.MapGet("/", () => "API OK");

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

app.Run();
