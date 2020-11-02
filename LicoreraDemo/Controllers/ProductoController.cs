using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LicoreraDemo.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Data.SqlClient.Server;

namespace LicoreraDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly FacturacionContext _context;

        public ProductoController(FacturacionContext context)
        {
            _context = context;
        }

        // GET: api/Productoes/activos
        [HttpGet("activos")]
        public JsonResult GetProductoActivos()
        {
            var producto = _context.Productos.Where(p => p.Activo == true).ToList();
            return new JsonResult(producto);
        }

        // GET: api/Producto
        [HttpGet]
        public IEnumerable<Producto> GetProductos()
        {
            return  _context.Productos.ToList();
        }
        // GET: api/Producto/Buscar/{valor}
        [HttpGet("Buscar/{valor}")]
        public JsonResult GetProductosByDescripcion(string valor)
        {
            var f = from Factura in _context.Facturas
                    join DetalleFactura in _context.DetalleFacturas
                    on Factura.Id equals DetalleFactura.FacturaId
                    where(Factura.Id == 1)
                    select new { Factura.NoFactura, DetalleFactura.ProductoId, DetalleFactura.Cantidad };

            var p = from Producto in _context.Productos
                    join DetalleFactura in _context.DetalleFacturas on Producto.Id equals DetalleFactura.ProductoId
                    join TasaCambio in _context.TasaCambios on DetalleFactura.TasaCambioId equals TasaCambio.Id
                    where Producto.Descripcion.Contains(valor)
                    select new { Producto.Id, Producto.Nombre, Producto.Descripcion, Producto.PreciosNio, Producto.PrecioUS, Producto.Stock, TasaCambio.Equivalencia };
            return new JsonResult(p);
        }

        // GET: api/Producto/{id}
        [HttpGet("{id}")]
        public ActionResult<Producto> GetProducto(int id)
        {
            var producto = _context.Productos.Find(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        [HttpPut("{id}")]
        public IActionResult PutProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Producto
        [HttpPost]
        public ActionResult<Producto> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            _context.SaveChanges();

            return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
        }

        // DELETE: api/Producto/5
        [HttpDelete("{id}")]
        public ActionResult<Producto> DeleteProducto(int id)
        {
            var producto = _context.Productos.Find(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Productos.Remove(producto);
            _context.SaveChanges();

            return producto;
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.Id == id);
        }
    }
}
