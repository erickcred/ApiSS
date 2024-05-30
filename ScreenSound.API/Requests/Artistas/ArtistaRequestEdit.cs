using ScreenSound.Modelos;
using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests.Artistas;

public record ArtistaRequestEdit(
  int Id,
  string Nome,
  string Bio,
  string? FotoPerfil) : ArtistaRequest(Nome, Bio);
