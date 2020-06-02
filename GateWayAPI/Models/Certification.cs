using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GateWayAPI.Models;

namespace GateWayAPI
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

        public Login Admin { get; set; }
        public Personne Professionnel { get; set; }

    } 
}
