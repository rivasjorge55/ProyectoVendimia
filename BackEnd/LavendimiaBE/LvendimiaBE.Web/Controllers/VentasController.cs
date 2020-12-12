using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LavendimiaBE.Datos;
using LavendimiaBE.Entidades.Registros;
using LvendimiaBE.Web.Models.Ventas;
using System.Data.SqlClient;

namespace LvendimiaBE.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly DbContextLavendimiaBE _context;

        public VentasController(DbContextLavendimiaBE context)
        {
            _context = context;
        }


        // GET: api/Ventas
        [HttpGet("[action]")]
        public async Task<IEnumerable<VentaViewModel>> Listar()
        {
            var Venta = await _context.Ventas.ToListAsync();
            return Venta.Select(a => new VentaViewModel
            {

                idVenta = a.idVenta,
                idCliente=  a.idCliente,
                total = a.total,
                fechaVenta = a.fechaVenta
            });
        }

        // GET: api/Ventas/opcionesCompra/4860
        [HttpGet("[action]/{monto}")]
        public async Task<IEnumerable<opcionesCompra>> opcionesCompra([FromRoute] decimal monto)
        {
            // Initialization.  
            List<opcionesCompra> lst = new List<opcionesCompra>();

            try
            {
                // Settings.  
                SqlParameter usernameParam = new SqlParameter("@monto", monto);

                // Processing.  
                string sqlQuery = "EXEC [dbo].[store_OpcionesVenta] @monto";

                lst = await _context.Query<opcionesCompra>().FromSql(sqlQuery, usernameParam).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // Info.  
            return lst;
        }


        // GET: api/Ventas/vw_Ventas
        [HttpGet("[action]")]
        public async Task<IEnumerable<vw_ventas>> vw_Ventas()
        {
            var ventas = await _context.vwVentas.ToListAsync();
            return ventas;
        }

        // GET: api/Ventas/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {
            var Venta = await _context.Ventas.FindAsync(id);

            if (Venta == null)
            {
                return NotFound();
            }

            return Ok(new VentaViewModel
            {
                idVenta = Venta.idVenta,
                idCliente = Venta.idCliente,
                total = Venta.total,
                fechaVenta = Venta.fechaVenta
            });
        }

        // PUT: api/Ventas/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarVentaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idVenta < 0)
            {
                return BadRequest();
            }

            var Venta = await _context.Ventas.FirstOrDefaultAsync(a => a.idVenta == model.idVenta);

            if (Venta == null)
            {
                return NotFound();
            }

            Venta.idVenta = model.idVenta;
            Venta.idCliente = Venta.idCliente;
            Venta.total = Venta.total;
            Venta.fechaVenta = Venta.fechaVenta;

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

        // POST: api/Ventas/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearVentaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Venta Venta = new Venta
            {

                idCliente = model.idCliente,
                total = model.total,
                fechaVenta = model.fechaVenta

            };

            _context.Ventas.Add(Venta);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/Ventas/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Venta = await _context.Ventas.FindAsync(id);
            if (Venta == null)
            {
                return NotFound();
            }

            _context.Ventas.Remove(Venta);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(Venta);
        }


        //// PUT: api/Ventas/Desactivar/5
        //[HttpPut("[action]/{id}")]
        //public async Task<IActionResult> Desactivar([FromRoute] int id)
        //{

        //    if (id < 0)
        //    {
        //        return BadRequest();
        //    }

        //    var Venta = await _context.Ventas.FirstOrDefaultAsync(c => c.idVenta == id);

        //    if (Venta == null)
        //    {
        //        return NotFound();
        //    }

        //    Venta.activo = false;

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


        //// PUT: api/Ventas/Activar/5
        //[HttpPut("[action]/{id}")]
        //public async Task<IActionResult> Activar([FromRoute] int id)
        //{

        //    if (id < 0)
        //    {
        //        return BadRequest();
        //    }

        //    var Venta = await _context.Ventas.FirstOrDefaultAsync(c => c.idVenta == id);

        //    if (Venta == null)
        //    {
        //        return NotFound();
        //    }

        //    Venta.activo = true;

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


        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.idVenta == id);
        }
    }
}