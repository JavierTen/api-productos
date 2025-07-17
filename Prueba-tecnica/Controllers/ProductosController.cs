using Microsoft.AspNetCore.Mvc;
using Prueba_tecnica.Data;
using Prueba_tecnica.Models;
using Prueba_tecnica.DTOs.Productos;
using Microsoft.EntityFrameworkCore;

namespace Prueba_tecnica.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CrearProducto([FromBody] CrearProductoDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            try
            {
                var producto = new Producto
                {
                    Nombre = productoDto.Nombre,
                    Descripcion = productoDto.Descripcion,
                    Precio = productoDto.Precio,
                    Estado = productoDto.Estado,
                    FechaCreacion = DateTime.UtcNow
                };
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(CrearProducto), new { id = producto.Id }, producto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor: " + ex.Message);

            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarProducto(int id, [FromBody] CrearProductoDto productoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var producto = await _context.Productos.FindAsync(id);

                if (producto == null)
                {
                    return NotFound(new { mensaje = "El producto con el ID especificado no existe." });
                }

                producto.Nombre = productoDto.Nombre;
                producto.Descripcion = productoDto.Descripcion;
                producto.Precio = productoDto.Precio;
                producto.Estado = productoDto.Estado;

                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Producto actualizado correctamente." });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al actualizar el producto.", detalle = ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> ListarProductos()
        {
            try
            {
                var productos = await _context.Productos
                    .Include(p => p.Imagenes)
                    .Select(p => new
                    {
                        p.Id,
                        p.Nombre,
                        p.Descripcion,
                        p.Precio,
                        p.FechaCreacion,
                        p.Estado,
                        Imagenes = p.Imagenes.Select(i => new
                        {
                            i.Id,
                            i.Url,
                            i.FechaSubida
                        }).ToList()
                    })
                    .ToListAsync();

                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al obtener los productos.",
                    detalle = ex.Message
                });
            }
        }


        [HttpGet("ordenados-precio")]
        public async Task<IActionResult> ListarProductosOrdenadosPorPrecio()
        {
            try
            {
                var productos = await _context.Productos
                    .Include(p => p.Imagenes)
                    .OrderBy(p => p.Precio)
                    .Select(p => new
                    {
                        p.Id,
                        p.Nombre,
                        p.Descripcion,
                        p.Precio,
                        p.FechaCreacion,
                        p.Estado,
                        Imagenes = p.Imagenes.Select(i => new
                        {
                            i.Id,
                            i.Url,
                            i.FechaSubida
                        }).ToList()
                    })
                    .ToListAsync();

                return Ok(productos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al obtener los productos ordenados.",
                    detalle = ex.Message
                });
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            try
            {
                var producto = await _context.Productos
                    .Include(p => p.Imagenes) 
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (producto == null)
                {
                    return NotFound(new { mensaje = "Producto no encontrado." });
                }

                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();

                return Ok(new { mensaje = "Producto eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    mensaje = "Error al eliminar el producto.",
                    detalle = ex.Message
                });
            }
        }
    }
}
