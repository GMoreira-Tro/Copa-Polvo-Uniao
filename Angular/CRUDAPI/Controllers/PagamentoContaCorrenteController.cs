using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRUDAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRUDAPI.Services;

namespace CRUDAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagamentoContaCorrenteController : ControllerBase
    {
        private readonly Contexto _contexto;
        private readonly PagamentoContaCorrenteService _pagamentoContaCorrenteService;

        public PagamentoContaCorrenteController(Contexto contexto, PagamentoContaCorrenteService pagamentoContaCorrenteService)
        {
            _contexto = contexto;
            _pagamentoContaCorrenteService = pagamentoContaCorrenteService;
        }

        // GET: api/PagamentoContaCorrente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagamentoContaCorrente>>> GetPagamentoContaCorrentes()
        {
            return await _contexto.PagamentoContaCorrentes.ToListAsync();
        }

        // GET: api/PagamentoContaCorrente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PagamentoContaCorrente>> GetPagamentoContaCorrente(long id)
        {
            var pagamentoContaCorrente = await _contexto.PagamentoContaCorrentes.FindAsync(id);

            if (pagamentoContaCorrente == null)
            {
                return NotFound();
            }

            return pagamentoContaCorrente;
        }

        // POST: api/PagamentoContaCorrente
        [HttpPost]
        public async Task<ActionResult<PagamentoContaCorrente>> PostPagamentoContaCorrente(PagamentoContaCorrente pagamentoContaCorrente)
        {
            try
            {
                pagamentoContaCorrente = await _pagamentoContaCorrenteService.ValidarPagamentoContaCorrente(pagamentoContaCorrente);

                _contexto.PagamentoContaCorrentes.Add(pagamentoContaCorrente);
                await _contexto.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPagamentoContaCorrente), new { id = pagamentoContaCorrente.Id }, pagamentoContaCorrente);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/PagamentoContaCorrente/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPagamentoContaCorrente(long id, PagamentoContaCorrente pagamentoContaCorrente)
        {
            if (id != pagamentoContaCorrente.Id)
            {
                return BadRequest();
            }

            _contexto.Entry(pagamentoContaCorrente).State = EntityState.Modified;

            try
            {
                pagamentoContaCorrente = await _pagamentoContaCorrenteService.ValidarPagamentoContaCorrente(pagamentoContaCorrente);
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_pagamentoContaCorrenteService.PagamentoContaCorrenteExists(id))
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

        // DELETE: api/PagamentoContaCorrente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePagamentoContaCorrente(long id)
        {
            var pagamentoContaCorrente = await _contexto.PagamentoContaCorrentes.FindAsync(id);
            if (pagamentoContaCorrente == null)
            {
                return NotFound();
            }

            _contexto.PagamentoContaCorrentes.Remove(pagamentoContaCorrente);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
