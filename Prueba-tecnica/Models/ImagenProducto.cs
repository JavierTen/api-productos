using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba_tecnica.Models
{
    public class ImagenProducto
    {
        public int Id { get; set; }

        [Required]
        public string Url { get; set; } = string.Empty;

        public DateTime FechaSubida { get; set; } = DateTime.UtcNow;

        // Relación con Producto
        [ForeignKey("Producto")]
        public int ProductoId { get; set; }

        public Producto? Producto { get; set; }
    }
}
