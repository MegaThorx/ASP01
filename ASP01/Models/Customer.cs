using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASP01.Models
{
    [Table("Customers", Schema = "cust")]
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        [Display(Name = "Vorname")]
        [StringLength(100)]
        public string FName { get; set; }

        [Display(Name = "Nachname")]
        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string LName { get; set; }

        [Display(Name = "Geburtsdatum")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        [Display(Name = "Rabatt")]
        [Range(0.0f, 100.0f)]
        public float Discount { get; set; }

        [Display(Name = "Notizen")]
        [StringLength(1000)]
        public string Notes { get; set; }

        [Display(Name = "Adresse")]
        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }

        public virtual ICollection<BasketItem> BasketItem { get; set; }
    }
}