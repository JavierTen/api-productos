using System.ComponentModel.DataAnnotations;

namespace Prueba_tecnica.DTOs.ImagenesProducto
{
    public class ImagenProductoCrearDto
    {
        [Required]
        public string Url { get; set; } = string.Empty;

        [Required]
        public int ProductoId { get; set; }
    }
}
