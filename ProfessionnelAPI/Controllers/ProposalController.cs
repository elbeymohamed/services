using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfessionnelAPI;
using ProfessionnelAPI.Data;

namespace ProfessionnelAPI.Controllers
{
    [Route("api/professionnel/[controller]")]
    [ApiController]
    public class ProposalController : ControllerBase
    {
        private readonly ProfessionnelContext _context;

        public ProposalController(ProfessionnelContext context)
        {
            _context = context;
        }

        // GET: api/Pro
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proposal>>> GetProposal()
        {
            var listGlobal = await _context.Proposals.ToListAsync();
            string email = Request.Headers["email"].First().ToString();
            var listProposals = new List<Proposal>();            

            foreach (Proposal proposal in listGlobal)
            {
                if (email.Equals(proposal.ProId))
                {
                    listProposals.Add(proposal);
                }
            }
            if (listProposals.Count == 0)
            {
                return Ok("Aucune proposition");
            }

            return listProposals;
        }

        // GET: api/Pro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proposal>> GetProposal(int id)
        {
            var proposal = await _context.Proposals.FindAsync(id);

            if (proposal == null)
            {
                return NotFound();
            }

            return proposal;
        }

        // PUT: api/Pro/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProposal(int id, Proposal proposal)
        {
            if (id != proposal.Id)
            {
                return BadRequest();
            }

            _context.Entry(proposal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProposalExists(id))
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

        // POST: api/Pro
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Proposal>> PostProposal(Proposal proposal)
        {
            _context.Proposals.Add(proposal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProposal", new { id = proposal.Id }, proposal);
        }

        // DELETE: api/Pro/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Proposal>> DeleteProposal(int id)
        {
            var proposal = await _context.Proposals.FindAsync(id);
            if (proposal == null)
            {
                return NotFound();
            }

            _context.Proposals.Remove(proposal);
            await _context.SaveChangesAsync();

            return proposal;
        }

        private bool ProposalExists(int id)
        {
            return _context.Proposals.Any(e => e.Id == id);
        }
    }
}
