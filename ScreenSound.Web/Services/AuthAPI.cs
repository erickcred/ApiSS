﻿using System.Net.Http.Json;
using ScreenSound.Web.Response;

namespace ScreenSound.Web.Services
{
    public class AuthAPI
  {
    private readonly HttpClient _httpClient;

    public AuthAPI(IHttpClientFactory factory)
    {
      _httpClient = factory.CreateClient("API");
    }

    public async Task<AuthResponse> LoginAsync(string email, string senha)
    {
      var response = await _httpClient.PostAsJsonAsync("auth/login", new { email, password = senha });

      if (response.IsSuccessStatusCode)
        return new AuthResponse { Sucesso = true };

      return new AuthResponse { Sucesso = false, Erros = ["Login ou Senha inválidos!"] };
    }
  }
}
