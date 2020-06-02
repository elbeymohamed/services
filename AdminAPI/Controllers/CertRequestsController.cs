using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminAPI;
using AdminAPI.Data;
using Microsoft.AspNetCore.Authorization;

namespace AdminAPI.Controllers
{
    [Route("api/admin/[controller]")]
    [ApiController]
    public class CertRequestsController : ControllerBase
    {
        private readonly AdminAPIContext _context;

        public CertRequestsController(AdminAPIContext context)
        {
            _context = context;
        }

        // GET: api/CertRequests
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CertRequest>>> GetCertRequest()
        {
           // return await _context.CertRequests.ToListAsync();
            return await _context.CertRequests.Include(d => d.Professional).ToListAsync();
        }

        // GET: api/CertRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CertRequest>> GetCertRequest(int id)
        {
            var certRequest = await _context.CertRequests.FindAsync(id);

            if (certRequest == null)
            {
                return NotFound();
            }

            return certRequest;
        }

        // PUT: api/CertRequests/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertRequest(int id, CertRequest certRequest)
        {
            if (id != certRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(certRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertRequestExists(id))
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

        // POST: api/CertRequests
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CertRequest>> PostCertRequest(CertRequest certRequest)
        {
            _context.CertRequests.Add(certRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCertRequest", new { id = certRequest.Id }, certRequest);
        }

        // DELETE: api/CertRequests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CertRequest>> DeleteCertRequest(int id)
        {
            var certRequest = await _context.CertRequests.FindAsync(id);
            if (certRequest == null)
            {
                return NotFound();
            }

            _context.CertRequests.Remove(certRequest);
            await _context.SaveChangesAsync();

            return certRequest;
        }

        private bool CertRequestExists(int id)
        {
            return _context.CertRequests.Any(e => e.Id == id);
        }
    }
}
