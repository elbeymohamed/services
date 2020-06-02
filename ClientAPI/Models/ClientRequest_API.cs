using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientAPI
{
    public class ClientRequest_API
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

        public string ClientId { get; set; }

        public int InterventionID { get; set; }
    }
}
