using ScreenSound.Web.Requests.Artistas;
using ScreenSound.Web.Response.Artista;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services
{
  public class ArtistaAPI
  {
    private readonly HttpClient _httpClient;

    public ArtistaAPI(IHttpClientFactory factory)
    {
      _httpClient = factory.CreateClient("API");
    }

    public async Task<ICollection<ArtistaResponse>> GetArtistasAsync()
    {
      var dados = await _httpClient.GetFromJsonAsync<ICollection<ArtistaResponse>>("artistas");
      return dados;
    }
    
    public async Task<ArtistaResponse> GetArtistaAsync(string nome)
    {
      var dado = await _httpClient.GetFromJsonAsync<ArtistaResponse>($"artistas/{nome}");
      return dado;
    }

    public async Task PostArtistaAsync(ArtistaRequest artista)
    {
      await _httpClient.PostAsJsonAsync("artistas", artista);
    }


    public async Task PutArtistaAsync(ArtistaRequestEdit artista, int id)
    {
      await _httpClient.PutAsJsonAsync($"artistas/{id}", artista);
    }

    public async Task DeleteArtistaAsync(int id)
    {
      await _httpClient.DeleteAsync($"artistas/{id}");
    }
  }
}
