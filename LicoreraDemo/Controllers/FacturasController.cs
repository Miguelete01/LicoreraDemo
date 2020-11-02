using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LicoreraDemo.Models;
using Microsoft.AspNetCore.Razor.Language.Extensions;
using System.Data.Entity.SqlServer;
using Microsoft.Extensions.Logging;

namespace LicoreraDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturasController : ControllerBase
    {
        private readonly FacturacionContext _context;

        public FacturasController(FacturacionContext context)
        {
            _context = context;
        }

        // GET: api/Facturas
        [HttpGet]
        public ActionResult<IEnumerable<Factura>> GetFacturas()
        {
            return _context.Facturas.ToList();
        }

        // GET: api/Facturas/Detalle/
        [HttpGet("Detalle")]
        public JsonResult GetFacturasDetaill()
        {
            var f = from Factura in _context.Facturas
                    join DetalleFactura in _context.DetalleFacturas
                    on Factura.Id equals DetalleFactura.FacturaId

                    select new { Factura.NoFactura, DetalleFactura.ProductoId, DetalleFactura.Cantidad };

            return new JsonResult(f);
        }

        // GET: api/Facturas/FacturasClienteId/1
        [HttpGet("FacturasClienteId/{Id}")]
        public JsonResult GetFacturasPorCliente(int Id)
        {
            var f = from Cliente in _context.Clientes
                    join Factura in _context.Facturas on Cliente.Id equals Factura.ClienteId
                    join DetalleFactura in _context.DetalleFacturas on Factura.Id equals DetalleFactura.FacturaId
                    join Producto in _context.Productos on DetalleFactura.ProductoId equals Producto.Id

                    where (Factura.Id == Id)
                    select new { Cliente.NoIdentificacion, Cliente.Nombre, Cliente.Apellidos, Factura.NoFactura,
                                 DetalleFactura.ProductoId, DetalleFactura.Cantidad, Producto.Descripcion, Producto.PreciosNio };

            return new JsonResult(f);
        }
        // GET: api/Facturas/FacturasNo/
        [HttpGet("FacturasNo/{NoFactura}")]
        public JsonResult GetFacturasPorNoFactura(string NoFactura)
        {
            var f = from Cliente in _context.Clientes
                    join Factura in _context.Facturas on Cliente.Id equals Factura.ClienteId
                    join DetalleFactura in _context.DetalleFacturas on Factura.Id equals DetalleFactura.FacturaId
                    join Producto in _context.Productos on DetalleFactura.ProductoId equals Producto.Id

                    where (Factura.NoFactura == NoFactura)
                    select new
                    {
                        Cliente.NoIdentificacion,
                        Cliente.Nombre,
                        Cliente.Apellidos,
                        Factura.NoFactura,
                        DetalleFactura.ProductoId,
                        DetalleFactura.Cantidad,
                        Producto.Descripcion,
                        Producto.PreciosNio
                    };

            return new JsonResult(f);
        }

        // GET: api/Facturas/ReporteMensual/{mes}
        [HttpGet("ReporteMensual/{mes}")]
        public JsonResult GetRptVentasMensual(int mes)
        {
            var f = from Cliente in _context.Clientes
                    join Factura in _context.Facturas on Cliente.Id equals Factura.ClienteId
                    join DetalleFactura in _context.DetalleFacturas on Factura.Id equals DetalleFactura.FacturaId
                    join Producto in _context.Productos on DetalleFactura.ProductoId equals Producto.Id
                    join TasaCambio in _context.TasaCambios on DetalleFactura.TasaCambioId equals TasaCambio.Id

                    where ( Factura.Fecha.Month == mes)

                    select new
                    {
                        Cliente.NoIdentificacion,
                        Cliente = Cliente.Nombre + ' ' + Cliente.Apellidos,
                        Producto.Descripcion,
                        Factura.NoFactura,
                        DetalleFactura.Cantidad,
                        Producto.PreciosNio,
                        SubTotal = DetalleFactura.Cantidad * Producto.PreciosNio,
                    };

            return new JsonResult(f);
        }

        // GET: api/Facturas/{id}
        [HttpGet("{id}")]
        public ActionResult<Factura> GetFactura(int id)
        {
            var factura = _context.Facturas.Find(id);

            if (factura == null)
            {
                return NotFound();
            }

            return factura;
        }

        // PUT: api/Facturas/{id}
        [HttpPut("{id}")]
        public IActionResult PutFactura(int id, Factura factura)
        {
            if (id != factura.Id)
            {
                return BadRequest();
            }

            _context.Entry(factura).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacturaExists(id))
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

        // POST: api/Facturas
        [HttpPost]
        public ActionResult<Factura> PostFactura(Factura factura)
        {
            using (var db = _context.Database.BeginTransaction())
            {
                try
                {
                    if (factura != null)
                    {
                        // INSERT
                        if (factura.Id == 0)
                        {
                            Factura f = new Factura();
                            f.ClienteId = factura.ClienteId;
                            f.NoFactura = factura.NoFactura;
                            f.Fecha = factura.Fecha;
                            _context.Facturas.Add(f);
                            _context.SaveChanges();
                            foreach (var dt in factura.DetalleFactura)
                            {
                                if(dt.Id == 0)// insert detalle nuevo
                                {
                                    DetalleFactura det = new DetalleFactura();
                                    det.FacturaId = f.Id;
                                    det.ProductoId = dt.ProductoId;
                                    det.TasaCambioId = dt.TasaCambioId;
                                    det.Cantidad = dt.Cantidad;
                                    _context.DetalleFacturas.Add(det);
                                } else // Update detalle Existente
                                {
                                    DetalleFactura det = new DetalleFactura();
                                    det.Id = dt.Id;
                                    det.FacturaId = f.Id;
                                    det.ProductoId = dt.ProductoId;
                                    det.TasaCambioId = dt.TasaCambioId;
                                    det.Cantidad = dt.Cantidad;
                                    _context.Entry(det).State = EntityState.Modified;
                                }
                            }
                            _context.SaveChanges();
                        } else //UPDATE
                        {
                            Factura f = new Factura();
                            f.ClienteId = factura.ClienteId;
                            f.NoFactura = factura.NoFactura;
                            f.Fecha = factura.Fecha;
                            f.Id = factura.Id;
                            _context.Entry(f).State = EntityState.Modified;
                            _context.SaveChanges();
                            foreach (var dt in factura.DetalleFactura)
                            {
                                if (dt.Id == 0)// insert detalle nuevo
                                {
                                    DetalleFactura det = new DetalleFactura();
                                    det.FacturaId = f.Id;
                                    det.ProductoId = dt.ProductoId;
                                    det.TasaCambioId = dt.TasaCambioId;
                                    det.Cantidad = dt.Cantidad;
                                    _context.DetalleFacturas.Add(det);
                                }
                                else // Update detalle Existente
                                {
                                    DetalleFactura det = new DetalleFactura();
                                    det.Id = dt.Id;
                                    det.FacturaId = f.Id;
                                    det.ProductoId = dt.ProductoId;
                                    det.TasaCambioId = dt.TasaCambioId;
                                    det.Cantidad = dt.Cantidad;
                                    _context.Entry(det).State = EntityState.Modified;
                                }
                            }
                            _context.SaveChanges();
                        }
                        db.Commit();    
                    }
                }
                catch(Exception ex)
                {
                    db.Rollback();
                }
            }
            //_context.Facturas.Add(factura);

            //_context.SaveChanges();
            return CreatedAtAction("GetFactura", new { id = factura.Id }, factura);
        }

        // DELETE: api/Facturas/5
        [HttpDelete("{id}")]
        public ActionResult<Factura> DeleteFactura(int id)
        {
            var factura = _context.Facturas.Find(id);
            if (factura == null)
            {
                return NotFound();
            }

            _context.Facturas.Remove(factura);
            _context.SaveChanges();

            return factura;
        }

        private bool FacturaExists(int id)
        {
            return _context.Facturas.Any(e => e.Id == id);
        }
    }
}
