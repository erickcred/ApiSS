using Microsoft.EntityFrameworkCore;
using ScreenSound.API.Endpoints;
using ScreenSound.API.Response.Artista;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using ScreenSound.Shared.Dados.Modelos;
using ScreenSound.Shared.Modelos.Modelos;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(
  options => options.AddPolicy(
    "wasm",
    policy => policy.WithOrigins([builder.Configuration["BackendUrl"] ?? "https://localhost:7051/",
    builder.Configuration["FontendUrl"] ?? "https://localhost:7084/"])
    .AllowAnyMethod()
    .SetIsOriginAllowed(pol => true)
    .AllowAnyHeader()
    .AllowCredentials()));

builder.Services.AddDbContext<ScreenSoundContext>(options =>
{
  options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
        .UseLazyLoadingProxies();
});

// Autenticação
builder.Services
  .AddIdentityApiEndpoints<PessoaComAcesso>()
  .AddEntityFrameworkStores<ScreenSoundContext>();

// Autorização
builder.Services.AddAuthorization();

builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();
builder.Services.AddTransient<DAL<Genero>>();
builder.Services.AddTransient<DAL<Discografia>>();


// Configuração Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Corrigir JsonException
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(
  options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.UseCors("wasm");

app.UseStaticFiles();

// Autorização 
app.UseAuthorization();

app.AddEndPointsArtistas();
app.AddEndpointMusicas();
app.AddEndpointGeneros();
app.AddEndpointDiscografia();

// Autenticação
app.MapGroup("auth")
  .MapIdentityApi<PessoaComAcesso>()
  .WithTags("Autorizacao");

app.UseSwagger();
app.UseSwaggerUI();




app.Run();
