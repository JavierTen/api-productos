namespace Prueba_tecnica.DTOs.ImagenesProducto
{
    public class ImagenProductoDto
    {
        public int Id { get; set; }

        public string Url { get; set; } = string.Empty;

        public DateTime FechaSubida { get; set; }

        public int ProductoId { get; set; }
    }
}
