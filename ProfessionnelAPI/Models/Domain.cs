using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProfessionnelAPI
{
    public class Domain
    {
        [Key]
        [Display(Name = " Domain name")]
        [StringLength(50, MinimumLength = 3)]
        public string DomainName { get; set; }

        public ICollection<Personne> Professionnels { get; set; }
        public ICollection<Intervention> Interventions { get; set; }
    }
}
