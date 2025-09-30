using FrontendRestauranteMarisco.WebApp.DTOs.PlatilloDTOs;

namespace FrontendRestauranteMarisco.WebApp.Services
{
    public class PlatilloService
    {
        private readonly HttpClient _http;

        // Aquí configuramos la ruta base de la API
        private const string baseUrl = "api/platillos";

        public PlatilloService(HttpClient http)
        {
            _http = http;
        }

        // Obtener lista de platillos
        public async Task<List<CrearPlatilloDTO>> GetPlatillosAsync()
        {
            return await _http.GetFromJsonAsync<List<CrearPlatilloDTO>>(baseUrl);
        }

        // Obtener lista de platillos con nombre de categoría
        public async Task<List<PlatilloConCategoriaDTO>> GetPlatillosConCategoriaAsync()
        {
            return await _http.GetFromJsonAsync<List<PlatilloConCategoriaDTO>>($"{baseUrl}/con-categoria");
        }

        // Crear un nuevo platillo
        public async Task<bool> CrearPlatilloAsync(CrearPlatilloDTO dto)
        {
            var response = await _http.PostAsJsonAsync(baseUrl, dto);
            return response.IsSuccessStatusCode;
        }

        // Actualizar un platillo
        public async Task<bool> ActualizarPlatilloAsync(ActualizarPlatilloDTO dto)
        {
            var response = await _http.PutAsJsonAsync($"{baseUrl}/{dto.Id}", dto);
            return response.IsSuccessStatusCode;
        }

        // Eliminar un platillo
        public async Task<bool> EliminarPlatilloAsync(int id)
        {
            var response = await _http.DeleteAsync($"{baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
