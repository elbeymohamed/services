using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProfessionnelAPI;

namespace ProfessionnelAPI.Data
{
    public class ProfessionnelContext : DbContext
    {
        public ProfessionnelContext (DbContextOptions<ProfessionnelContext> options)
            : base(options)
        {
        }

        public DbSet<ProfessionnelAPI.Proposal> Proposals { get; set; }

        public DbSet<ProfessionnelAPI.Personne> Personne { get; set; }

        public DbSet<ProfessionnelAPI.Certification> Certification { get; set; }

        public DbSet<ProfessionnelAPI.CertRequest> CertRequest { get; set; }

        public DbSet<ProfessionnelAPI.ClientRequest> ClientRequest { get; set; }

        public DbSet<ProfessionnelAPI.Evaluation> Evaluation { get; set; }

        public DbSet<ProfessionnelAPI.Domain> Domains { get; set; }
        public DbSet<ProfessionnelAPI.Intervention> Interventions { get; set; }

        public DbSet<ProfessionnelAPI.Models.Login> Login { get; set; }
        public DbSet<InterventionRequest> InterventionRequests { get; set; }
    }
}
