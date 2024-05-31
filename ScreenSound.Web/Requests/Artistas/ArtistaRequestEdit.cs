namespace ScreenSound.Web.Requests.Artistas;

public record ArtistaRequestEdit(
  int Id,
  string Nome,
  string Bio,
  string? FotoPerfil) : ArtistaRequest(Nome, Bio, FotoPerfil);
