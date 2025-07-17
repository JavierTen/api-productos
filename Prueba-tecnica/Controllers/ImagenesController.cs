using Microsoft.AspNetCore.Mvc;
using Prueba_tecnica.Data;
using Prueba_tecnica.Models;
using Prueba_tecnica.DTOs.Productos;
using Prueba_tecnica.DTOs.ImagenesProducto;
using Microsoft.EntityFrameworkCore;


namespace Prueba_tecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagenesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ImagenesController(AppDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<IActionResult> AgregarImagen([FromBody] ImagenProductoCrearDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var producto = await _context.Productos.FindAsync(dto.ProductoId);
                if (producto == null)
                {
                    return NotFound(new { mensaje = "El producto no existe." });
                }

                var imagen = new ImagenProducto
                {
                    Url = dto.Url,
                    ProductoId = dto.ProductoId,
                    FechaSubida = DateTime.UtcNow
                };

                _context.ImagenesProductos.Add(imagen);
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Imagen agregada correctamente.", imagenId = imagen.Id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al agregar la imagen.",
                    detalle = ex.Message
                });
            }
        }


        [HttpGet]
        public async Task<IActionResult> ListarImagenes()
        {
            try
            {
                var imagenes = await _context.ImagenesProductos
                    .Select(i => new
                    {
                        i.Id,
                        i.Url,
                        i.FechaSubida,
                        i.ProductoId
                    })
                    .ToListAsync();

                return Ok(imagenes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al obtener las imágenes.",
                    detalle = ex.Message
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarImagen(int id, [FromBody] ImagenProductoCrearDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var imagen = await _context.ImagenesProductos.FindAsync(id);
                if (imagen == null)
                {
                    return NotFound(new { mensaje = "Imagen no encontrada." });
                }

                var producto = await _context.Productos.FindAsync(dto.ProductoId);
                if (producto == null)
                {
                    return BadRequest(new { mensaje = "El producto especificado no existe." });
                }

                imagen.Url = dto.Url;

                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Imagen actualizada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al editar la imagen.",
                    detalle = ex.Message
                });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarImagen(int id)
        {
            try
            {
                var imagen = await _context.ImagenesProductos.FindAsync(id);
                if (imagen == null)
                {
                    return NotFound(new { mensaje = "Imagen no encontrada." });
                }

                _context.ImagenesProductos.Remove(imagen);
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Imagen eliminada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al eliminar la imagen.",
                    detalle = ex.Message
                });
            }
        }




    }
}
