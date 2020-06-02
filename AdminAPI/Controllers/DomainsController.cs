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
    public class DomainsController : ControllerBase
    {
        private readonly AdminAPIContext _context;

        public DomainsController(AdminAPIContext context)
        {
            _context = context;
        }

        // GET: api/Domains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Domain>>> GetDomain()
        {
            return await _context.Domains.ToListAsync();
        }

        // GET: api/Domains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Domain>> GetDomain(string id)
        {
            var domain = await _context.Domains.FindAsync(id);

            if (domain == null)
            {
                return NotFound();
            }

            return domain;
        }

        // PUT: api/Domains/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDomain(string id, Domain domain)
        {
            if (id != domain.DomainName)
            {
                return BadRequest();
            }

            _context.Entry(domain).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DomainExists(id))
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

        // POST: api/Domains
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Domain>> PostDomain(Domain domain)
        {
            _context.Domains.Add(domain);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DomainExists(domain.DomainName))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDomain", new { id = domain.DomainName }, domain);
        }

        // DELETE: api/Domains/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Domain>> DeleteDomain(string id)
        {
            var domain = await _context.Domains.FindAsync(id);
            if (domain == null)
            {
                return NotFound();
            }

            _context.Domains.Remove(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        private bool DomainExists(string id)
        {
            return _context.Domains.Any(e => e.DomainName == id);
        }
    }
}
