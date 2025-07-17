using System.ComponentModel.DataAnnotations;

namespace Prueba_tecnica.DTOs.Productos
{
    public class CrearProductoDto
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [MaxLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres.")]
        [MinLength(1, ErrorMessage = "La descripción no puede estar vacía.")]
        public string Descripcion { get; set; } = string.Empty;


        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal Precio { get; set; }

        public bool Estado { get; set; } = true;
    }
}
