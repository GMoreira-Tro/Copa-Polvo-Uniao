using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDAPI.Models;
using CRUDAPI.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRUDAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtletaController : ControllerBase
    {
        private readonly Contexto _contexto;
        private readonly AtletaService _atletaService;

        public AtletaController(Contexto contexto, AtletaService atletaService)
        {
            _contexto = contexto;
            _atletaService = atletaService;
        }

        // GET: api/Atleta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Atleta>>> GetAtletas()
        {
            return await _contexto.Atletas.ToListAsync();
        }

        // GET: api/Atleta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Atleta>> GetAtleta(long id)
        {
            var atleta = await _contexto.Atletas.FindAsync(id);

            if (atleta == null)
            {
                return NotFound();
            }

            return atleta;
        }

        // PUT: api/Atleta/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAtleta(long id, Atleta atleta)
        {
            if (id != atleta.Id)
            {
                return BadRequest();
            }

            _contexto.Entry(atleta).State = EntityState.Modified;

            try
            {
                atleta = await _atletaService.ValidarAtleta(atleta);
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_atletaService.AtletaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }

        // POST: api/Atleta
        [HttpPost]
        public async Task<ActionResult<Atleta>> PostAtleta(Atleta atleta)
        {
            try
            {
                atleta = await _atletaService.ValidarAtleta(atleta);

                _contexto.Atletas.Add(atleta);
                await _contexto.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAtleta), new { id = atleta.Id }, atleta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Atleta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAtleta(long id)
        {
            var atleta = await _contexto.Atletas.FindAsync(id);
            if (atleta == null)
            {
                return NotFound();
            }

            _contexto.Atletas.Remove(atleta);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
