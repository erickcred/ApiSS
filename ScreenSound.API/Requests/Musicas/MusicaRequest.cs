using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests.Musicas;
public record MusicaRequest(
  [Required] string Nome,
  [Required] int ArtistaId,
  int AnoLancamento,
  ICollection<GeneroRequest> Generos = null,
  ICollection<DiscografiaRequest> Discografias = null);