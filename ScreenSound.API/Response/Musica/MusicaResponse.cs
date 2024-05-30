namespace ScreenSound.API.Response.Musica;

public record MusicaResponse(
  int Id,
  string Nome,
  int ArtistaId,
  string NomeArtista);
