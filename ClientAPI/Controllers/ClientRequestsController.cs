using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientAPI;
using ClientAPI.Data;
using Microsoft.AspNetCore.Authorization;
using ClientAPI.DataProvider;

namespace ClientAPI.Controllers
{
    
    [Route("api/client/[controller]")]
    [ApiController]
    public class ClientRequestsController : ControllerBase
    {
        private readonly ClientAPIContext _context;

        public ClientRequestsController(ClientAPIContext context)
        {
            _context = context;
        }

        // GET: api/ClientRequests
        [Authorize]
        //[Authorize(Roles = "Client")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientRequest>>> GetClientRequest()
        {
            return await _context.ClientRequest.ToListAsync();
        }

        // GET: api/ClientRequests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientRequest>> GetClientRequest(int id)
        {
            var clientRequest = await _context.ClientRequest.FindAsync(id);

            if (clientRequest == null)
            {
                return NotFound();
            }

            return clientRequest;
        }

        // PUT: api/ClientRequests/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientRequest(int id, ClientRequest clientRequest)
        {
            if (id != clientRequest.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientRequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientRequestExists(id))
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

        // POST: api/ClientRequests
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ClientRequest>> PostClientRequest(ClientRequest_API clientRequestAPI)
        {
            try {
                ClientRequest clientRequest = new ClientRequest();
                clientRequest.ClientId = clientRequestAPI.ClientId;
                clientRequest.Description = clientRequestAPI.Description;
                clientRequest.Title = clientRequestAPI.Title;
                clientRequest.RequestDate = new DateTime();
                clientRequest.status = "En Attente";
      
                _context.ClientRequest.Add(clientRequest);

                await _context.SaveChangesAsync();

                InterventionRequest interventionRequest = new InterventionRequest();
                interventionRequest.ClientRequestId = clientRequest.Id;
                interventionRequest.InterventionId = clientRequestAPI.InterventionID;

                _context.InterventionRequests.Add(interventionRequest);

                await _context.SaveChangesAsync();
            }
            catch
            {
                return NotFound("Problem database");
            }

            return CreatedAtAction("GetClientRequest", new { id = clientRequestAPI.Id }, clientRequestAPI);
        }

        // DELETE: api/ClientRequests/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClientRequest>> DeleteClientRequest(int id)
        {
            var clientRequest = await _context.ClientRequest.FindAsync(id);
            if (clientRequest == null)
            {
                return NotFound();
            }

            _context.ClientRequest.Remove(clientRequest);
            await _context.SaveChangesAsync();

            return clientRequest;
        }

        /// <summary>
        /// get information from Professionnal API, Like list domains etc
        /// 
        /// </summary>
        /// <returns> List of domains </returns>
        // Get list of domains 
        // GET: api/ClientRequests/Domains
        [Authorize]
        [HttpGet("/domains")]
        public async Task<ActionResult<List<Domain>>> GetDomains()
        {
            try
            {
                var auth = Request.Headers["Authorization"];

                // var authHeader = Request.Headers.Get("Authorization");
                return await ProfessionalAPIProvider.getDomains(auth);
            }
            catch
            {
                return NotFound();
            }
        }

        // Get list of Interventions 
        // GET: api/ClientRequests/Interventions
        [Authorize]
        [HttpGet("/interventions/{id}")]
        public async Task<ActionResult<IEnumerable<Intervention>>> GetInterventions(string id)
        {
           var listInterventions = await _context.Intervention.ToListAsync();
            listInterventions.FindAll(x => x.Domain.DomainName.Equals(id));
            return listInterventions;
        }

        private bool ClientRequestExists(int id)
        {
            return _context.ClientRequest.Any(e => e.Id == id);
        }

    }
}
