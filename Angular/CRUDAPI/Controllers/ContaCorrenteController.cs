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
    public class ContaCorrenteController : ControllerBase
    {
        private readonly Contexto _contexto;
        private readonly ContaCorrenteService _contaCorrenteService;

        public ContaCorrenteController(Contexto contexto, ContaCorrenteService contaCorrenteService)
        {
            _contexto = contexto;
            _contaCorrenteService = contaCorrenteService;
        }

        // GET: api/ContaCorrente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContaCorrente>>> GetContaCorrentes()
        {
            return await _contexto.ContasCorrentes.ToListAsync();
        }

        // GET: api/ContaCorrente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContaCorrente>> GetContaCorrente(long id)
        {
            var contaCorrente = await _contexto.ContasCorrentes.FindAsync(id);

            if (contaCorrente == null)
            {
                return NotFound();
            }

            return contaCorrente;
        }

        // PUT: api/ContaCorrente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContaCorrente(long id, ContaCorrente contaCorrente)
        {
            if (id != contaCorrente.Id)
            {
                return BadRequest();
            }

            _contexto.Entry(contaCorrente).State = EntityState.Modified;

            try
            {
                contaCorrente = await _contaCorrenteService.ValidarContaCorrente(contaCorrente);
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_contaCorrenteService.ContaCorrenteExists(id))
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

        // POST: api/ContaCorrente
        [HttpPost]
        public async Task<ActionResult<ContaCorrente>> PostContaCorrente(ContaCorrente contaCorrente)
        {
            try
            {
                contaCorrente = await _contaCorrenteService.ValidarContaCorrente(contaCorrente);

                _contexto.ContasCorrentes.Add(contaCorrente);
                await _contexto.SaveChangesAsync();

                return CreatedAtAction(nameof(GetContaCorrente), new { id = contaCorrente.Id }, contaCorrente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ContaCorrente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContaCorrente(long id)
        {
            var contaCorrente = await _contexto.ContasCorrentes.FindAsync(id);
            if (contaCorrente == null)
            {
                return NotFound();
            }

            _contexto.ContasCorrentes.Remove(contaCorrente);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
