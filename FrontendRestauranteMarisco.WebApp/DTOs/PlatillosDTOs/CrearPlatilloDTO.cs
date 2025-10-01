using System.ComponentModel.DataAnnotations;

namespace FrontendRestauranteMarisco.WebApp.DTOs.PlatilloDTOs
{
    public class CrearPlatilloDTO
    {
        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }


        public int CategoriaId { get; set; }
    }
}