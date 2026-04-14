using ProyectoFILHA.Models.DTOs;
using ProyectoFILHA.Services.Interfaces;
using System.Text.Json;

public class DragonBallService : IDragonBallService
{
    private readonly HttpClient _httpClient;

    public DragonBallService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<DragonBallResponse> ObtenerPersonajes()
    {
        var url = "https://dragonball-api.com/api/characters/";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
            throw new Exception("Error al consumir API Dragon Ball");

        var json = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<DragonBallResponse>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}