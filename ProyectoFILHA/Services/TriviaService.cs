using ProyectoFILHA.Models.DTOs;
using ProyectoFILHA.Services.Interfaces;
using System.Text.Json;

public class TriviaService : ITriviaService
{
    private readonly HttpClient _httpClient;

    public TriviaService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TriviaResponse> ObtenerPreguntas()
    {
        var url = "https://opentdb.com/api.php?amount=10&category=14&difficulty=medium";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Error al consumir API");

        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<TriviaResponse>(json);
    }
}