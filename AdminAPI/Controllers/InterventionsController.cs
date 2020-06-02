using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminAPI;
using AdminAPI.Data;

namespace AdminAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterventionsController : ControllerBase
    {
        private readonly AdminAPIContext _context;

        public InterventionsController(AdminAPIContext context)
        {
            _context = context;
        }

        // GET: api/Interventions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetIntervention()
        {
            return await _context.Intervention.ToListAsync();
        }

        // GET: api/Interventions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Intervention>> GetIntervention(int id)
        {
            var intervention = await _context.Intervention.FindAsync(id);

            if (intervention == null)
            {
                return NotFound();
            }

            return intervention;
        }

        // PUT: api/Interventions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntervention(int id, Intervention intervention)
        {
            if (id != intervention.Id)
            {
                return BadRequest();
            }

            _context.Entry(intervention).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterventionExists(id))
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

        // POST: api/Interventions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Intervention>> PostIntervention(Intervention intervention)
        {
            _context.Intervention.Add(intervention);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIntervention", new { id = intervention.Id }, intervention);
        }

        // DELETE: api/Interventions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Intervention>> DeleteIntervention(int id)
        {
            var intervention = await _context.Intervention.FindAsync(id);
            if (intervention == null)
            {
                return NotFound();
            }

            _context.Intervention.Remove(intervention);
            await _context.SaveChangesAsync();

            return intervention;
        }

        private bool InterventionExists(int id)
        {
            return _context.Intervention.Any(e => e.Id == id);
        }
    }
}
