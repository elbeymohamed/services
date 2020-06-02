using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfessionnelAPI
{
    public class InterventionRequest
    {
        public int Id { get; set; }

        public int InterventionId { get; set; }
        [Required]
        [ForeignKey("InterventionId")]
        public Intervention Intervention { get; set; }

        public int ClientRequestId { get; set; }
        [Required]
        [ForeignKey("ClientRequestId")]
        public ClientRequest ClientRequest { get; set; }
    }
}
