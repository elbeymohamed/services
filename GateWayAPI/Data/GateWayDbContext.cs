using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GateWayAPI.Models;

namespace GateWayAPI.Data
{
    public class GateWayDbContext : DbContext
    {
        public GateWayDbContext (DbContextOptions<GateWayDbContext> options)
            : base(options)
        {
        }

        public DbSet<Login> Login { get; set; }
        public DbSet<Personne> Personnes { get; set; }

        public DbSet<Certification> Certifications { get; set; }

        public DbSet<CertRequest> CertRequests { get; set; }

        public DbSet<Domain> Domains { get; set; }

        public DbSet<Evaluation> Evaluations { get; set; }

        public DbSet<Intervention> Interventions { get; set; }

        public DbSet<InterventionRequest> InterventionRequests { get; set; }

        public DbSet<Proposal> Proposals { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<InterventionRequest>()
        //        .HasKey(c => new { c.ClientRequest.Id, c.Intervention.Id });
        //}

    }
}
