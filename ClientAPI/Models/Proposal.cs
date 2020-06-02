using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientAPI
{
    public class Proposal
    {
        public int  Id { get; set; }

    [Display(Name = " Professional")]
    [ForeignKey("ProId")]
    public Personne Professional { get; set; }

    [Display(Name = " Client Request")]
    [ForeignKey("ClientRequestId")]
    public ClientRequest ClientRequest { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Estimation { get; set; }

    [Required]
    [Display(Name = " Estimation date")]
    [DataType(DataType.Date)]
    public DateTime EstimationDate { get; set; }

    [Display(Name = " Message")]
    [StringLength(140, MinimumLength = 0)]
    public string Message { get; set; }

    }
}
