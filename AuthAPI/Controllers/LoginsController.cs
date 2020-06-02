using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthAPI.Data;
using AuthAPI.Models;
using Microsoft.Extensions.Configuration;
using AuthAPI.Services;
using Microsoft.Extensions.Primitives;

namespace AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly AuthAPIContext _context;
        private IConfiguration configuration;

        public LoginsController(AuthAPIContext context, IConfiguration iConfig)
        {
            _context = context;
            configuration = iConfig;
        }

        /*This method is used to get token for user using username and password ! */
        [HttpGet]
        public async Task<IActionResult> GetLogin()
        {
            try
            {
                string email = Request.Headers["email"].First();
                string password = Request.Headers["password"].ToString();

                var response = await LoginsProvider.GetToken(_context, configuration, email, password);
                if (response.StatusCode == 200) // to be modified
                {
                   return Ok(response); // to be modified not authentifed code
                }
                else if(response.StatusCode ==400)
                {
                    return Unauthorized("UnAuthorized");
                }
                else
                {
                    return NotFound("User not found");
                }

            }
            catch
            {
                return NotFound("catch clause");
            }
        }


        // POST: api/Logins
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Login>> PostLogin(Login login)
        {
            _context.Login.Add(login);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoginExists(login.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLogin", new { id = login.Email }, login);
        }


        /* to verify if all below methods are necessary, i think no ! */


        /*   // GET: api/Logins
           [HttpGet]
           public async Task<ActionResult<IEnumerable<Login>>> GetLogin()
           {
               return await _context.Login.ToListAsync();
           }

           // GET: api/Logins/5
           [HttpGet("{id}")]
           public async Task<ActionResult<Login>> GetLogin(string id)
           {
               var login = await _context.Login.FindAsync(id);

               if (login == null)
               {
                   return NotFound();
               }

               return login;
           }

           // PUT: api/Logins/5
           // To protect from overposting attacks, please enable the specific properties you want to bind to, for
           // more details see https://aka.ms/RazorPagesCRUD.
           [HttpPut("{id}")]
           public async Task<IActionResult> PutLogin(string id, Login login)
           {
               if (id != login.Email)
               {
                   return BadRequest();
               }

               _context.Entry(login).State = EntityState.Modified;

               try
               {
                   await _context.SaveChangesAsync();
               }
               catch (DbUpdateConcurrencyException)
               {
                   if (!LoginExists(id))
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


           // DELETE: api/Logins/5
           [HttpDelete("{id}")]
           public async Task<ActionResult<Login>> DeleteLogin(string id)
           {
               var login = await _context.Login.FindAsync(id);
               if (login == null)
               {
                   return NotFound();
               }

               _context.Login.Remove(login);
               await _context.SaveChangesAsync();

               return login;
           }
*/
           private bool LoginExists(string id)
           {
               return _context.Login.Any(e => e.Email == id);
           }
           
    }
}
