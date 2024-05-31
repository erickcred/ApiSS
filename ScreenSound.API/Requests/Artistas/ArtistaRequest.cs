using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests.Artistas;

public record ArtistaRequest(
  [Required] string Nome,
  [Required] string Bio,
  string? FotoPerfil);

