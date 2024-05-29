using ScreenSound.Modelos;
using System.ComponentModel.DataAnnotations;

namespace ScreenSound.API.Requests.Musicas;

public record MusicaRequestEdit(
  int Id,
  [Required] string Nome,
  [Required] int ArtistaId,
  int? AnoLancamento,
  ICollection<GeneroRequestEdit> Generos = null,
  ICollection<DiscografiaRequestEdit> Discografias = null);
