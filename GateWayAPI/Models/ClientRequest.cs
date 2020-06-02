using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GateWayAPI
{
    public class ClientRequest
    {

        public int Id { get; set; }

        [Required]
        [Display(Name = " Request title")]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Display(Name = " Description")]
        [StringLength(100, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        [Display(Name = " Request date")]
        [DataType(DataType.Date)]
        public DateTime RequestDate { get; set; }

        [Required]
        [Display(Name = " Status")]
        [StringLength(100, MinimumLength = 3)]
        public string status { get; set; }

        [Required]
        [Display(Name = " Client")]
        [ForeignKey("ClientId")]
        public Personne Client { get; set; }

        public ICollection<Proposal> Proposals { get; set; }
        public ICollection<Evaluation> Evaluations { get; set; }
    }
}
