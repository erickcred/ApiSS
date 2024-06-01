using ScreenSound.Web.Response.Musica.Generos;
using System.Net.Http.Json;

namespace ScreenSound.Web.Services
{
  public class GeneroAPI
  {
    private readonly HttpClient _httpClient;

    public GeneroAPI(IHttpClientFactory factory)
    {
      _httpClient = factory.CreateClient("API");
    }

    public async Task<ICollection<GeneroResponse>> GetGenerosAsync()
    {
      var dados = await _httpClient.GetFromJsonAsync<ICollection<GeneroResponse>>("generos");
      return dados;
    }

    public async Task<GeneroResponse?> GetGeneroAsync(string nome)
    {
      return await _httpClient.GetFromJsonAsync<GeneroResponse>($"generos/{nome}");
    }
  }
}
