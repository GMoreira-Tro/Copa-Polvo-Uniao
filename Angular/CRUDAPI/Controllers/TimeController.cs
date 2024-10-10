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
    public class TimeController : ControllerBase
    {
        private readonly Contexto _contexto;
        private readonly TimeService _timeService;

        public TimeController(Contexto contexto, TimeService timeService)
        {
            _contexto = contexto;
            _timeService = timeService;
        }

        // GET: api/Time
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Time>>> GetTimes()
        {
            return await _contexto.Times.ToListAsync();
        }

        // GET: api/Time/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Time>> GetTime(long id)
        {
            var time = await _contexto.Times.FindAsync(id);

            if (time == null)
            {
                return NotFound();
            }

            return time;
        }

        // PUT: api/Time/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTime(long id, Time time)
        {
            if (id != time.Id)
            {
                return BadRequest();
            }

            _contexto.Entry(time).State = EntityState.Modified;

            try
            {
                time = await _timeService.ValidarTime(time);
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_timeService.TimeExists(id))
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

        // POST: api/Time
        [HttpPost]
        public async Task<ActionResult<Time>> PostTime(Time time)
        {
            try
            {
                time = await _timeService.ValidarTime(time);

                _contexto.Times.Add(time);
                await _contexto.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTime), new { id = time.Id }, time);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Time/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTime(long id)
        {
            var time = await _contexto.Times.FindAsync(id);
            if (time == null)
            {
                return NotFound();
            }

            _contexto.Times.Remove(time);
            await _contexto.SaveChangesAsync();

            return NoContent();
        }
    }
}
