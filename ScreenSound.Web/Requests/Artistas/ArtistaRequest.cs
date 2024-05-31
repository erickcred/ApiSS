using System.ComponentModel.DataAnnotations;

namespace ScreenSound.Web.Requests.Artistas;

public record ArtistaRequest(
  [Required] string Nome,
  [Required] string Bio,
  string? FotoPerfil);

