using System.ComponentModel.DataAnnotations;
using ScreenSound.Web.Requests.Musicas.Discografias;
using ScreenSound.Web.Requests.Musicas.Generos;

namespace ScreenSound.Web.Requests.Musicas;
public record MusicaRequest(
  [Required] string Nome,
  [Required] int ArtistaId,
  int AnoLancamento,
  ICollection<GeneroRequest> Generos = null,
  ICollection<DiscografiaRequest> Discografias = null);