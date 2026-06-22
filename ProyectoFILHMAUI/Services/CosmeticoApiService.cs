using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http.Json;
using ProyectoFILHMAUI.Models;

namespace ProyectoFILHMAUI.Services
{
    public class CosmeticoApiService
    {
        private readonly HttpClient _httpClient;

        public CosmeticoApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Cosmetico>> GetCosmeticosAsync()
        {
            var response = await _httpClient.GetAsync("api/cosmeticos");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<Cosmetico>>();
            return data ?? new List<Cosmetico>();
        }
    }
}
