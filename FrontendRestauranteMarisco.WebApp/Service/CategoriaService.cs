using FrontendRestauranteMarisco.WebApp.DTOs.CategoriaDTOS;

namespace FrontendRestauranteMarisco.WebApp.Service
{
    public class CategoriaService
    {
        private readonly HttpClient _http;

        private readonly ApiService _api;
        private const string Base = "Categoria";

        public CategoriaService(ApiService api) => _api = api;

        // Listar todos los cargos
        public async Task<List<CategoriaRespuestaDTO>?> GetAllAsync(string? token = null)
        {
            return await _api.GetAllAsync<CategoriaRespuestaDTO>(Base, token);
        }

        //Listar un cargo por id
        public async Task<CategoriaRespuestaDTO?> GetByIdAsync(int id, string? token = null)
        {
            return await _api.GetByIdAsync<CategoriaRespuestaDTO>(Base, id, token);
        }
        // Crear un nuevo cargo
        public async Task<CategoriaRespuestaDTO> CreateAsync(CategoriaCreateDTO dto, string token)
        {
            return await _api.PostAsync<CategoriaCreateDTO, CategoriaRespuestaDTO>(Base, dto, token);
        }

        // Actualizar un cargo por id
        public async Task<bool> UpdateAsync(int Id_Categoria, CategoriaActualizarDTO dto, string token)
        {
            try
            {
                await _api.PutAsync<CategoriaActualizarDTO, CategoriaRespuestaDTO>(Base, Id_Categoria, dto, token);
                return true;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }


        // Eliminar un cargo por id
        public async Task<bool> DeleteAsync(int Id_Categoria, string token)
        {
            return await _api.DeleteAsync(Base, Id_Categoria, token);
        }

    }
}
