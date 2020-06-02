using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdminAPI.Models;

namespace AdminAPI
{
    public class Certification
    {
        public int Id { get; set; }

        [Display(Name = " Certification date")]
        [DataType(DataType.Date)]
        public DateTime CertificationDate { get; set; }

        //[Required]
        //public int IdAdmin { get; set; }

        //[Required]
        //public int IdPro { get; set; }
        public string ProfessionnelEmail { get; set; }
        [ForeignKey("ProfessionnelEmail")]
        public Personne Professionnel { get; set; }

        public string AdminEmail { get; set; }
        [ForeignKey("AdminEmail")]
        public Login Admin { get; set; }
        

        

    } 
}
