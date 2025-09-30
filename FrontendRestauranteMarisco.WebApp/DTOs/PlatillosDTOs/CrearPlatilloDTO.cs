using System.ComponentModel.DataAnnotations;

namespace FrontendRestauranteMarisco.WebApp.DTOs.PlatilloDTOs
{
    public class CrearPlatilloDTO
    {
        public int Id { get; set; }   // 🔹 Necesario para las acciones Editar / Detalles / Eliminar

        [Required(ErrorMessage = "El nombre del platillo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Nombre { get; set; }

        [StringLength(250, ErrorMessage = "La descripción no puede superar los 250 caracteres.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio.")]
        [Range(0, 10000, ErrorMessage = "El precio debe estar entre 0 y 10000.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        public int CategoriaId { get; set; }

        public bool Activo { get; set; }   // 🔹 Para mostrar "Sí" o "No" en la vista
    }
}