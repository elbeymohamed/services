using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdminAPI.Models;
using AdminAPI;

namespace AdminAPI.Data
{
    public class AdminAPIContext : DbContext
    {
        public AdminAPIContext (DbContextOptions<AdminAPIContext> options)
            : base(options)
        {
        }

        public DbSet<AdminAPI.Models.Login> Login { get; set; }

        public DbSet<AdminAPI.Certification> Certifications { get; set; }

        public DbSet<AdminAPI.CertRequest> CertRequests { get; set; }

        public DbSet<AdminAPI.Domain> Domains { get; set; }

        public DbSet<AdminAPI.Intervention> Intervention { get; set; }

        public DbSet<AdminAPI.Personne> Personnes { get; set; }
    }
}
