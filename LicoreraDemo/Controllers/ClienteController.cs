using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LicoreraDemo.Models;

namespace LicoreraDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly FacturacionContext _context;

        public ClienteController(FacturacionContext context)
        {
            _context = context;
        }

        // GET: api/Cliente
        [HttpGet]
        public IEnumerable<Cliente> GetClientes()
        {
            return  _context.Clientes.ToList();
        }

        // GET: api/Cliente/{id}
        [HttpGet("{id}")]
        public ActionResult<Cliente> GetCliente(int id)
        {
            var cliente = _context.Clientes.Find(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // GET: api/Cliente/nombre/{nombre}
        [HttpGet("nombre/{nombre}")]
        public IEnumerable<Cliente> GetClienteByName(string nombre)
        {
            return _context.Clientes.Where(c => c.Nombre == nombre).ToList();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        public ActionResult<Cliente> PostCliente(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
             _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.Id }, cliente);
        }

        // DELETE: api/Cliente/{id}
        [HttpDelete("{id}")]
        public ActionResult<Cliente> DeleteCliente(int id)
        {
            var cliente =  _context.Clientes.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            _context.SaveChangesAsync();

            return cliente;
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
