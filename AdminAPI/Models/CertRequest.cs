using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminAPI
{
    public class CertRequest
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = " Request date")]
        [DataType(DataType.Date)]
        public DateTime CertRequestDate { get; set; }

        [Required]

        public Personne Professional { get; set; }

    }
}