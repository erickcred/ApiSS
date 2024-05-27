using ScreenSound.Modelos;

namespace ScreenSound.API.Requests.Musicas;

public record MusicaRequestEdit(string Nome, int? AnoLancamento, Artista? Artista);
