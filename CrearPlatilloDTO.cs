namespace FrontendRestauranteMarisco.WebApp.DTOs.PlatilloDTOs
{
    public class CrearPlatilloDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int CategoriaId { get; set; }
        
    }
}
