namespace ScreenSound.Web.Requests.Musicas;

public record MusicaRequestEdit(
  int Id,
  string Nome,
  int ArtistaId,
  int AnoLancamento) : MusicaRequest(Nome, ArtistaId, AnoLancamento);
