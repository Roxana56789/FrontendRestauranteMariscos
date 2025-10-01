using System.ComponentModel.DataAnnotations;

namespace FrontendRestauranteMarisco.WebApp.DTOs.PlatilloDTOs
{
    public class RespuestaPlatilloDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }


        public int CategoriaId { get; set; }
    }
}
