
using Prueba_tecnica.DTOs.ImagenesProducto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Prueba_tecnica.DTOs.Productos
{
    public class ProductoDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public decimal Precio { get; set; }

        public DateTime FechaCreacion { get; set; }

        public bool Estado { get; set; }

        public List<ImagenProductoDto> Imagenes { get; set; } = new();
    }
}
