using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClientAPI.Models
{
    public class Login
    {
        [Key]
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [StringLength(50, MinimumLength = 5)]
        public string Email { get; set; }


        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Role { get; set; }
    }
}
