using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthAPI
{
    public class Personne
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

        [StringLength(50, MinimumLength = 3)]
        public string City { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string State { get; set; }

        [StringLength(6, MinimumLength = 2)]
        public int NCivic { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Street { get; set; }

        [StringLength(6, MinimumLength = 6)]
        public string ZipCode { get; set; }

        [StringLength(5, MinimumLength = 1)]
        public string Apartment { get; set; }

        [Phone]
        [StringLength(10, MinimumLength = 10)]
        public string Phone { get; set; }


        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string TypeUser { get; set; }

        [Required]
        [Display(Name = " Registration date")]
        [DataType(DataType.Date)]
        public DateTime RegistrationDate { get; set; }

    }

}
