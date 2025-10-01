using FrontendRestauranteMarisco.WebApp.DTOs.PlatilloDTOs;
using FrontendRestauranteMarisco.WebApp.Service;

namespace FrontendRestauranteMarisco.WebApp.Services
{
    public class PlatilloService
    {
        private readonly ApiService _api;
        private const string Base = "Platillo";

        public PlatilloService(ApiService api) => _api = api;

        public async Task<List<RespuestaPlatilloDTO>?> GetAllAsync(string? token = null)
        {
            return await _api.GetAllAsync<RespuestaPlatilloDTO>(Base, token);
        }

        public async Task<RespuestaPlatilloDTO?> GetByIdAsync(int id, string? token = null)
        {
            return await _api.GetByIdAsync<RespuestaPlatilloDTO>(Base, id, token);
        }

        public async Task<RespuestaPlatilloDTO> CreateAsync(CrearPlatilloDTO dto, string token)
        {
            return await _api.PostAsync<CrearPlatilloDTO, RespuestaPlatilloDTO>(Base, dto, token);
        }

        public async Task<bool> UpdateAsync(int id, ActualizarPlatilloDTO dto, string token)
        {
            try
            {
                await _api.PutAsync<ActualizarPlatilloDTO, RespuestaPlatilloDTO>(Base, id, dto, token);
                return true; // Si la llamada es exitosa, retorna true.
            }
            catch (HttpRequestException)
            {
                return false;  // Captura la excepción si la solicitud no fue exitosa (por ejemplo, 404 Not Found o 400 Bad Request).
            }
        }

        public async Task<bool> DeleteAsync(int id, string token)
        {
            return await _api.DeleteAsync(Base, id, token);
        }
    }
}
