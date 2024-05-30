namespace ScreenSound.API.Response.Artista
{
    public record ArtistaResponse(
      int Id,
      string Nome,
      string Bio,
      string? FotoPerfil);
}
