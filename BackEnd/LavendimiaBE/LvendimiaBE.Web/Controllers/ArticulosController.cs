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
    public class ArticulosController : ControllerBase
    {
        private readonly DbContextLavendimiaBE _context;

        public ArticulosController(DbContextLavendimiaBE context)
        {
            _context = context;
        }

        // GET: api/Articulos
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticuloViewModel>> Listar()
        {
            var articulo = await _context.Articulos.ToListAsync();
            return articulo.Select(a => new ArticuloViewModel
            {

                idArticulo = a.idArticulo,
                descripcion = a.descripcion,
                modelo = a.modelo,
                precio = a.precio,
                existencia = a.existencia,
                activo = a.activo
            });
        }

        // GET: api/Articulos/Mostrar/5
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Mostrar([FromRoute] int id)
        {
            var articulo = await _context.Articulos.FindAsync(id);

            if (articulo == null)
            {
                return NotFound();
            }

            return Ok(new ArticuloViewModel
            {
                idArticulo = articulo.idArticulo,
                descripcion = articulo.descripcion,
                modelo = articulo.modelo,
                precio = articulo.precio,
                existencia = articulo.existencia,
                activo = articulo.activo
            });
        }

        // PUT: api/Articulos/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ActualizarArticuloViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idArticulo < 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(a => a.idArticulo == model.idArticulo);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.idArticulo = model.idArticulo;
            articulo.descripcion = model.descripcion;
            articulo.modelo = model.modelo;
            articulo.precio = model.precio;
            articulo.existencia = model.existencia;
            articulo.activo = model.activo;

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

        // POST: api/Articulos/Crear
        [HttpPost("[action]")]
        public async Task<IActionResult> Crear([FromBody] CrearArticuloViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Articulo articulo = new Articulo
            {

                descripcion = model.descripcion,
                modelo = model.modelo,
                precio = model.precio,
                existencia = model.existencia,
                activo = true

            };

            _context.Articulos.Add(articulo);

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

        // DELETE: api/Articulos/Eliminar/5
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> Eliminar([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var articulo = await _context.Articulos.FindAsync(id);
            if (articulo == null)
            {
                return NotFound();
            }

            _context.Articulos.Remove(articulo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return Ok(articulo);
        }


        // PUT: api/Articulos/Desactivar/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Desactivar([FromRoute] int id)
        {

            if (id < 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(c => c.idArticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.activo = false;

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


        // PUT: api/Articulos/Activar/5
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> Activar([FromRoute] int id)
        {

            if (id < 0)
            {
                return BadRequest();
            }

            var articulo = await _context.Articulos.FirstOrDefaultAsync(c => c.idArticulo == id);

            if (articulo == null)
            {
                return NotFound();
            }

            articulo.activo = true;

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


        private bool ArticuloExists(int id)
        {
            return _context.Articulos.Any(e => e.idArticulo == id);
        }
    }
}