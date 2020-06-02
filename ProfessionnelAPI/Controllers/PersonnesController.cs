using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProfessionnelAPI;
using ProfessionnelAPI.Data;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Authorization;
using ProfessionnelAPI.Models;

namespace ProfessionnelAPI.Controllers
{
    [Route("api/professionnel/[controller]")]
    [ApiController]
    public class PersonnesController : ControllerBase
    {
        private readonly ProfessionnelContext _context;

        public PersonnesController(ProfessionnelContext context)
        {
            _context = context;
        }

        private bool AuthenticateKey()
        {
            var allowedKeys = new[] { "Secret@123", "Secret#12", "SecretABC" };
            StringValues key = Request.Headers["Key"];
            int count = (from t in allowedKeys where t == key select t).Count();
            return count == 0 ? false : true;
        }

        // GET: api/Personnes
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Login>>> GetPersonne()
        {
            string role = Request.Headers["role"].First().ToString();

            var listPersons = await _context.Login.ToListAsync();
            var listProfessionnals =new List<Login>();

            foreach (Login person in listPersons)
            {
                if (person.Role.Equals(role))
                {
                    listProfessionnals.Add(person);
                }
            }
            if (listProfessionnals.Count == 0)
            {
                return NotFound(listPersons);
            }

            return listProfessionnals;
        }

        // GET: api/Personnes/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Personne>> GetPersonne(string id)
        {
            var personne = await _context.Personne.FindAsync(id);

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
            _context.Personne.Add(personne);
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
            var personne = await _context.Personne.FindAsync(id);
            if (personne == null)
            {
                return NotFound();
            }

            _context.Personne.Remove(personne);
            await _context.SaveChangesAsync();

            return personne;
        }

        private bool PersonneExists(string id)
        {
            return _context.Personne.Any(e => e.Email == id);
        }

        private bool AreEquals(string test, string role)
        {
            if (role == test) return true;
            else return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_context);
        }
    }
}
