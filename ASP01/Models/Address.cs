using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP01.Models
{
    public class Address
    {
        [Key]
        public int AddressId { get; set; }

        [Display(Name = "Straße")]
        [StringLength(128)]
        public string Street { get; set; }

        [Display(Name = "Stadt")]
        [StringLength(128)]
        public string City { get; set; }

        [Display(Name = "Postleitzahl")]
        [StringLength(16)]
        public string PostalCode { get; set; }

        [Display(Name = "Land")]
        [StringLength(128)]
        public string Country { get; set; }
    }
}