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

    public async Task<ICollection<MusicaResponse>> GetGenerosAsync()
    {
      var dados = await _httpClient.GetFromJsonAsync<ICollection<MusicaResponse>>("generos");
      return dados;
    }

    public async Task<MusicaResponse?> GetGeneroAsync(string nome)
    {
      return await _httpClient.GetFromJsonAsync<MusicaResponse>($"generos/{nome}");
    }
  }
}
