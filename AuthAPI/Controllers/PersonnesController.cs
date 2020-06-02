using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthAPI;
using AuthAPI.Data;
using System.Security.Claims;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnesController : ControllerBase
    {
        private readonly AuthAPIContext _context;

        public PersonnesController(AuthAPIContext context)
        {
            _context = context;
        }

        // GET: api/Personnes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Personne>>> GetPersonne()
        {
            return await _context.Personnes.ToListAsync();
        }

        // GET: api/Personnes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Personne>> GetPersonne(string id)
        {
            var personne = await _context.Personnes.FindAsync(id);

            if (personne == null)
            {
                return NotFound();
            }

            return personne;
        }

        // PUT: api/Personnes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonne(string id, Personne personne)
        {
            if (id != personne.Email)
            {
                return BadRequest();
            }

            _context.Entry(personne).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonneExists(id))
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

        // POST: api/Personnes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Personne>> PostPersonne(Personne personne)
        {

            _context.Personnes.Add(personne);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PersonneExists(personne.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPersonne", new { id = personne.Email }, personne);
        }

        // DELETE: api/Personnes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Personne>> DeletePersonne(string id)
        {
            var personne = await _context.Personnes.FindAsync(id);
            if (personne == null)
            {
                return NotFound();
            }

            _context.Personnes.Remove(personne);
            await _context.SaveChangesAsync();

            return personne;
        }

        private bool PersonneExists(string id)
        {
            return _context.Personnes.Any(e => e.Email == id);
        }
    }
}
