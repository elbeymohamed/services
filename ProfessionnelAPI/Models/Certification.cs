using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfessionnelAPI
{
    public class Certification
    {
        public int Id { get; set; }

        [Display(Name = " Certification date")]
        [DataType(DataType.Date)]
        public DateTime CertificationDate { get; set; }

        public Personne Professionnel { get; set; }

    } 
}
