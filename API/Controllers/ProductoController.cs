using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Dtos;
using Models.Entidades;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // [HttpGet]
        // public ActionResult<List<ProductoDto>> GetProductos()
        // {
        //     return ProductoData.listaProducto
        //     .Select(p => new ProductoDto
        //     {
        //         NombreProducto = p.NombreProducto,
        //         Categoria = p.Categoria,
        //         Marca = p.Marca,
        //         Precio = p.Precio,
        //         Costo = p.Costo
        //     }
        //     ).ToList();
        // }

        [HttpGet]
        public async Task<ActionResult<List<ProductoDto>>> GetProductos()
        {
            List<ProductoDto> lista = await _context.Productos
                                        .Include(c => c.Categoria)
                                        .Include(m => m.Marca)
                                        .Select(p => new ProductoDto
                                        {
                                            NombreProducto = p.NombreProducto,
                                            Categoria = p.Categoria.Nombre,
                                            Marca = p.Marca.Nombre,
                                            Precio = p.Precio,
                                            Costo = p.Costo
                                        })
                                        .ToListAsync();
            return Ok(lista);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            Producto producto = await _context.Productos
                                        .Include(c => c.Categoria)
                                        .Include(m => m.Marca)
                                        .FirstOrDefaultAsync(p => p.Id == id);
            if (producto == null)
            {
                return NotFound();
            }
            return Ok(new ProductoDto
            {
                NombreProducto = producto.NombreProducto,
                Categoria = producto.Categoria.Nombre,
                Marca = producto.Marca.Nombre,
                Precio = producto.Precio,
                Costo = producto.Costo
            });
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> PostInvoice(Producto producto)
        {
            try
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            Producto productoBD = await _context.Productos.FindAsync(id);
            if (productoBD == null)
            {
                return NotFound();
            }
            productoBD.NombreProducto = producto.NombreProducto;
            productoBD.CategoriaId = producto.CategoriaId;
            productoBD.MarcaId = producto.MarcaId;
            productoBD.Costo = producto.Costo;
            productoBD.Precio = producto.Precio;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            Producto producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}