using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GateWayAPI
{
    public class InterventionRequest
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("InterventionId")]
        public Intervention Intervention { get; set; }

        [Required]
        [ForeignKey("ClientRequestId")]
        public ClientRequest ClientRequest { get; set; }
    }
}
