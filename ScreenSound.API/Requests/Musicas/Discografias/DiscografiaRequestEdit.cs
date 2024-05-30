namespace ScreenSound.API.Requests.Musicas.Discografias;

public record DiscografiaRequestEdit(
  int Id,
  string Nome,
  string Descricao);