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
    public class ConfiguracionesController : ControllerBase
    {
        private readonly DbContextLavendimiaBE _context;

        public ConfiguracionesController(DbContextLavendimiaBE context)
        {
            _context = context;
        }


        // GET: api/Configuraciones/Listar
        [HttpGet("[action]")]
        public async Task<IEnumerable<ConfiguracionViewModel>> Listar()
        {
            var cliente = await _context.configuracion.ToListAsync();
            return cliente.Select(c => new ConfiguracionViewModel
            {

                idConfiguracion = c.idConfiguracion,
                plazoMaximo = c.plazoMaximo,
                porcentajeEnganche = c.porcentajeEnganche,
                tazaFinanciamiento = c.tazaFinanciamiento

            });
        }

        // PUT: api/Configuraciones/Actualizar
        [HttpPut("[action]")]
        public async Task<IActionResult> Actualizar([FromBody] ConfiguracionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.idConfiguracion < 0)
            {
                return BadRequest();
            }

            var configuracion = await _context.configuracion.FirstOrDefaultAsync(c => c.idConfiguracion == model.idConfiguracion);

            if (configuracion == null)
            {
                return NotFound();
            }

            configuracion.idConfiguracion = model.idConfiguracion;
            configuracion.plazoMaximo = model.plazoMaximo;
            configuracion.porcentajeEnganche = model.porcentajeEnganche;
            configuracion.tazaFinanciamiento = model.tazaFinanciamiento;

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

        private bool ConfiguracionExists(int id)
        {
            return _context.configuracion.Any(e => e.idConfiguracion == id);
        }
    }
}