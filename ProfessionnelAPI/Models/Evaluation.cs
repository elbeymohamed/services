using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfessionnelAPI
{
    public class Evaluation
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = " Note")]
        public int Note { get; set; }

        [Display(Name = " Comment")]
        [StringLength(140, MinimumLength = 0)]
        public string Comment { get; set; }

        [Required]
        [Display(Name = " Evaluation date")]
        [DataType(DataType.Date)]
        public DateTime EvaluationDate { get; set; }

        [Required]
        [Display(Name = " Client Request")]
        [ForeignKey("ClientRequestId")]
        public ClientRequest ClientRequest { get; set; }
    }
}
