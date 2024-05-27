using ScreenSound.Modelos;

namespace ScreenSound.API.Requests.Artistas;

public record ArtistaRequestEdit(string Nome, string Bio, string? FotoPerfil, Musica? Musica);
