using ScreenSound.Web.Requests.Musicas;
using ScreenSound.Web.Response.Musica;
using ScreenSound.Web.Response.Musica.Generos;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services
{
  public class MusicaAPI
  {
    private readonly HttpClient _httpClient;

    public MusicaAPI(IHttpClientFactory factory)
    {
      _httpClient = factory.CreateClient("API");
    }

    public async Task<ICollection<MusicaResponse>> GetMusicasAsync()
    {
      var dados = await _httpClient.GetFromJsonAsync<ICollection<MusicaResponse>>("Musicas");
      return dados;
    }

    public async Task<MusicaResponse> GetMusicaAsync(string nome)
    {
      var dado = await _httpClient.GetFromJsonAsync<MusicaResponse>($"Musicas/{nome}");
      return dado;
    }

    public async Task<MusicaResponse?> GetGeneroAsync(string nome)
    {
      return await _httpClient.GetFromJsonAsync<MusicaResponse>($"Musicas/{nome}");
    }

    public async Task PostMusicaAsync(MusicaRequest musica)
    {
      await _httpClient.PostAsJsonAsync($"Musicas", musica);
    }

    public async Task PutMusicaAsync(MusicaRequest musica, int id)
    {
      await _httpClient.PutAsJsonAsync($"Musicas/{id}", musica);
    }

    public async Task DeleteMusicaAsync(int id)
    {
      await _httpClient.DeleteAsync($"Musicas/{id}");
    }
  }
}
