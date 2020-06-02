using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientAPI;
using ClientAPI.Data;

namespace ClientAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvaluationsController : ControllerBase
    {
        private readonly ClientAPIContext _context;

        public EvaluationsController(ClientAPIContext context)
        {
            _context = context;
        }

        // GET: api/Evaluations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evaluation>>> GetEvaluation()
        {
            return await _context.Evaluation.ToListAsync();
        }

        // GET: api/Evaluations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Evaluation>> GetEvaluation(int id)
        {
            var evaluation = await _context.Evaluation.FindAsync(id);

            if (evaluation == null)
            {
                return NotFound();
            }

            return evaluation;
        }

        // PUT: api/Evaluations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvaluation(int id, Evaluation evaluation)
        {
            if (id != evaluation.Id)
            {
                return BadRequest();
            }

            _context.Entry(evaluation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvaluationExists(id))
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

        // POST: api/Evaluations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Evaluation>> PostEvaluation(Evaluation evaluation)
        {
            _context.Evaluation.Add(evaluation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEvaluation", new { id = evaluation.Id }, evaluation);
        }

        // DELETE: api/Evaluations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Evaluation>> DeleteEvaluation(int id)
        {
            var evaluation = await _context.Evaluation.FindAsync(id);
            if (evaluation == null)
            {
                return NotFound();
            }

            _context.Evaluation.Remove(evaluation);
            await _context.SaveChangesAsync();

            return evaluation;
        }

        private bool EvaluationExists(int id)
        {
            return _context.Evaluation.Any(e => e.Id == id);
        }
    }
}
