using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GateWayAPI
{
    public class Intervention
    {

        public int Id { get; set; }

        [Display(Name = " Intervention name")]
        [StringLength(50, MinimumLength = 3)]
        public string InterventionName { get; set; }

        [Display(Name = " Description")]
        [StringLength(50, MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        [ForeignKey("DomainName")]
        public Domain Domain { get; set; }
    }
}
