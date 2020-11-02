using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LicoreraDemo.Models;
using Microsoft.VisualBasic;
using System.IO;

namespace LicoreraDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasaCambioController : ControllerBase
    {
        private readonly FacturacionContext _context;

        public TasaCambioController(FacturacionContext context)
        {
            _context = context;
        }

        // GET: api/TasaCambio
        [HttpGet]
        public ActionResult<IEnumerable<TasaCambio>> GetTasaCambios()
        {
            return _context.TasaCambios.ToList();
        }
        // GET: api/TasaCambio/TasaDiaria
        [HttpGet("TasaDiaria")]
        public JsonResult GetTasaDiaria()
        {
            DateTime fechaActual = DateTime.Now;
            var tasa = from TasaCambio in _context.TasaCambios
                       where TasaCambio.Fecha.Date == fechaActual.Date
                       select new { TasaCambio.Id, TasaCambio.Equivalencia, TasaCambio.Fecha };
            return new JsonResult(tasa);
        }
        // Con fecha proporcionada
        // GET: api/TasaCambio/TasaDiaria
        [HttpGet("TasaDiaria/{fecha}")]
        public JsonResult GetTasaDiaria(DateTime fecha)
        {
            var tasa = from TasaCambio in _context.TasaCambios
                       where TasaCambio.Fecha.Date == fecha.Date
                       select new { TasaCambio.Id, TasaCambio.Equivalencia, TasaCambio.Fecha };
            return new JsonResult(tasa);
        }
        // GET: api/TasaCambio/TasaMes
        [HttpGet("TasaMes")]
        public JsonResult GetTasaListMes()
        {
            DateTime fechaActual = DateTime.Now;
            var tasa = from TasaCambio in _context.TasaCambios
                       where (TasaCambio.Fecha.Month == fechaActual.Month && TasaCambio.Fecha.Year == fechaActual.Year)
                       select new { 
                           TasaCambio.Id,
                           TasaCambio.Equivalencia,
                           Fecha=  TasaCambio.Fecha.Date 
                       };
            return new JsonResult(tasa);
        }
        // Con Fecha Proporcionada
        // GET: api/TasaCambio/TasaMes
        [HttpGet("TasaMes/{fecha}")]
        public JsonResult GetTasaListMes(DateTime fecha)
        {
            var tasa = from TasaCambio in _context.TasaCambios
                       where (TasaCambio.Fecha.Month == fecha.Month && TasaCambio.Fecha.Year == fecha.Year)
                       select new
                       {
                           TasaCambio.Id,
                           TasaCambio.Equivalencia,
                           Fecha = TasaCambio.Fecha.Date
                       };
            return new JsonResult(tasa);
        }
        // GET: api/TasaCambio/{id}
        [HttpGet("{id}")]
        public ActionResult<TasaCambio> GetTasaCambio(int id)
        {
            var tasaCambio = _context.TasaCambios.Find(id);

            if (tasaCambio == null)
            {
                return NotFound();
            }

            return tasaCambio;
        }

        // PUT: api/TasaCambio/{id}
        [HttpPut("{id}")]
        public IActionResult PutTasaCambio(int id, TasaCambio tasaCambio)
        {
            if (id != tasaCambio.Id)
            {
                return BadRequest();
            }

            _context.Entry(tasaCambio).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TasaCambioExists(id))
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

        // POST: api/TasaCambio
        [HttpPost]
        public ActionResult<TasaCambio> PostTasaCambio(TasaCambio tasaCambio)
        {
            _context.TasaCambios.Add(tasaCambio);
            _context.SaveChanges();

            return CreatedAtAction("GetTasaCambio", new { id = tasaCambio.Id }, tasaCambio);
        }

        // DELETE: api/TasaCambio/5
        [HttpDelete("{id}")]
        public ActionResult<TasaCambio> DeleteTasaCambio(int id)
        {
            var tasaCambio = _context.TasaCambios.Find(id);
            if (tasaCambio == null)
            {
                return NotFound();
            }

            _context.TasaCambios.Remove(tasaCambio);
            _context.SaveChanges();

            return tasaCambio;
        }

        private bool TasaCambioExists(int id)
        {
            return _context.TasaCambios.Any(e => e.Id == id);
        }
    }
}
