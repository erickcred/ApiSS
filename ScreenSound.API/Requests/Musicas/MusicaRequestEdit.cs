using ScreenSound.Modelos;
using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests.Musicas;

public record MusicaRequestEdit(
  int Id,
  string Nome,
  int ArtistaId,
  int AnoLancamento) : MusicaRequest(Nome, ArtistaId, AnoLancamento);
