using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ClientAPI;
using ClientAPI.Models;

namespace ClientAPI.Data
{
    public class ClientAPIContext : DbContext
    {
        public ClientAPIContext (DbContextOptions<ClientAPIContext> options)
            : base(options)
        {
        }

        public DbSet<ClientRequest> ClientRequest { get; set; }

        public DbSet<Evaluation> Evaluation { get; set; }

        public DbSet<Login> Login { get; set; }

        public DbSet<Domain> Domains { get; set; }

        public DbSet<Intervention> Intervention { get; set; }

        public DbSet<InterventionRequest> InterventionRequests { get; set; }

        public DbSet<ClientAPI.Personne> Personnes { get; set; }
    }
}
