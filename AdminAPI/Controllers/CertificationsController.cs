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
    [Route("api/Admin/[controller]")]
    [ApiController]
    public class CertificationsController : ControllerBase
    {
        private readonly AdminAPIContext _context;

        public CertificationsController(AdminAPIContext context)
        {
            _context = context;
        }

        // GET: api/Certifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Certification>>> GetCertification()
        {
            return await _context.Certifications.Include(c =>  c.Professionnel ).ToListAsync();
        }

        // GET: api/Certifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Certification>> GetCertification(int id)
        {
            var certification = await _context.Certifications.FindAsync(id);

            if (certification == null)
            {
                return NotFound();
            }

            return certification;
        }

        // PUT: api/Certifications/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertification(int id, Certification certification)
        {
            if (id != certification.Id)
            {
                return BadRequest();
            }

            _context.Entry(certification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificationExists(id))
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

        // POST: api/Certifications
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Certification>> PostCertification(Certification certification)
        {
           // certification.ProfessionnelEmail = certification.Professionnel.Email;
             
            _context.Certifications.Add(certification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCertification", new { id = certification.Id }, certification);
        }

        // DELETE: api/Certifications/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Certification>> DeleteCertification(int id)
        {
            var certification = await _context.Certifications.FindAsync(id);
            if (certification == null)
            {
                return NotFound();
            }

            _context.Certifications.Remove(certification);
            await _context.SaveChangesAsync();

            return certification;
        }

        private bool CertificationExists(int id)
        {
            return _context.Certifications.Any(e => e.Id == id);
        }

        public static Certification approuverCertification(Personne personne) {

            Certification certification = new Certification();
            certification.Id=5;

            return certification;
        }
    }
}
