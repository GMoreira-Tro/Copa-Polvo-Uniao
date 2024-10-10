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
    public class ModalidadeController : ControllerBase
    {
        private readonly Contexto _contexto;
        private readonly ModalidadeService _modalidadeService;

        public ModalidadeController(Contexto contexto, ModalidadeService modalidadeService)
        {
            _contexto = contexto;
            _modalidadeService = modalidadeService;
        }

        // GET: api/Modalidade
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Modalidade>>> GetModalidades()
        {
            return await _contexto.Modalidades.ToListAsync();
        }

        // GET: api/Modalidade/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Modalidade>> GetModalidade(long id)
        {
            var modalidade = await _contexto.Modalidades.FindAsync(id);

            if (modalidade == null)
            {
                return NotFound();
            }

            return modalidade;
        }

        // PUT: api/Modalidade/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutModalidade(long id, Modalidade modalidade)
        {
            if (id != modalidade.Id)
            {
                return BadRequest();
            }

            _contexto.Entry(modalidade).State = EntityState.Modified;

            try
            {
                modalidade = await _modalidadeService.ValidarModalidade(modalidade);
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_modalidadeService.ModalidadeExists(id))
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

        // POST: api/Modalidade
        [HttpPost]
        public async Task<ActionResult<Modalidade>> PostModalidade(Modalidade modalidade)
        {
            try
            {
                modalidade = await _modalidadeService.ValidarModalidade(modalidade);

                _contexto.Modalidades.Add(modalidade);
                await _contexto.SaveChangesAsync();

                return CreatedAtAction(nameof(GetModalidade), new { id = modalidade.Id }, modalidade);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Modalidade/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModalidade(long id)
        {
            var modalidade = await _contexto.Modalidades.FindAsync(id);
            if (modalidade == null)
            {
                return NotFound();
            }

            _contexto.Modalidades.Remove(modalidade);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
