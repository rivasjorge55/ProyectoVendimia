using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LavendimiaBE.Datos;
using LavendimiaBE.Entidades.Catalogos;
using LvendimiaBE.Web.Models.Catalogos;

namespace LvendimiaBE.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly DbContextLavendimiaBE _context;

        public ClientesController(DbContextLavendimiaBE context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet("[action]")]
        public async Task  <IEnumerable<ClienteViewModel>> Listar()
        {
            var cliente = await _context.Clientes.ToListAsync();
            return cliente.Select(c => new ClienteViewModel
            {

                idCliente = c.idCliente,
                nombre = c.nombre,
                primerApellido = c.primerApellido,
                segundoApellido = c.segundoApellido,
                nombreCompleto = c.nombre + " " + c.primerApellido + " " + c.segundoApellido,
                rfc = c.rfc,
            }) ;
        }

        // GET: api/Clientes/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
                      
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(new ClienteViewModel
            {
                idCliente = cliente.idCliente,
                nombre = cliente.nombre,
                primerApellido = cliente.primerApellido,
                segundoApellido = cliente.segundoApellido,
                rfc = cliente.rfc,
            });
        }

        // PUT: api/Clientes/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar( [FromBody] ActualizarArticulosViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idCliente <0)
            {
                return BadRequest();
            }

            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.idCliente == model.idCliente);

            if (cliente == null)
            {
                return NotFound();
            }

            cliente.nombre = model.nombre;
            cliente.primerApellido = model.primerApellido;
            cliente.segundoApellido = model.segundoApellido;
            cliente.rfc = model.rfc;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //Guardar Excepcion
                return BadRequest();
            }

            return Ok();
        }

        // POST: api/Clientes/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearClienteViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Cliente cliente = new Cliente
            {
                nombre = model.nombre,
                primerApellido = model.primerApellido,
                segundoApellido = model.segundoApellido,
                rfc = model.rfc

            };

            _context.Clientes.Add(cliente);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/Clientes/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(cliente);
        }

        //NO HAY ESTADOS EN ESTE PROYECTO
        //// PUT: api/Clientes/Desactivar/5
        //[HttpPut("[action]/{id}")]
        //public async Task<IActionResult> Desactivar([FromRoute] int id)
        //{

        //    if (id < 0)
        //    {
        //        return BadRequest();
        //    }

        //    var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.idCliente == id);

        //    if (cliente == null)
        //    {
        //        return NotFound();
        //    }

        //    //cliente.condicion = false;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        //Guardar Excepcion
        //        return BadRequest();
        //    }

        //    return Ok();
        //}


        //// PUT: api/Clientes/Activar/5
        //[HttpPut("[action]/{id}")]
        //public async Task<IActionResult> Activar([FromRoute] int id)
        //{

        //    if (id < 0)
        //    {
        //        return BadRequest();
        //    }

        //    var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.idCliente == id);

        //    if (cliente == null)
        //    {
        //        return NotFound();
        //    }

        //    //cliente.condicion = true;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        //Guardar Excepcion
        //        return BadRequest();
        //    }

        //    return Ok();
        //}

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.idCliente == id);
        }
    }
}