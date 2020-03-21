using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASP01.Models
{
    [Table("Invoices", Schema = "cust")]
    public class Invoice
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Rechnung")]
        public int InvoiceId { get; set; }

        [Display(Name = "Kunde")]
        public int? CustomerId { get; set; }

        [Display(Name = "Bestellung")]
        public int? OrderId { get; set; }

        [Display(Name = "Rechnungsdatum")]
        [Required]
        public DateTime InvoiceDate { get; set; }

        [Display(Name = "Kopie")]
        [Required]
        public bool Copy { get; set; }

        [Display(Name = "Stoniert")]
        [Required]
        public bool Canceled { get; set; }

        [Display(Name = "Rabatt")]
        [Range(0.0f, 100.0f)]
        [Required]
        public float Discount { get; set; }
        
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

        [Display(Name = "Total")]
        public decimal Total => Math.Round((Positions.Sum(p => p.Sum)) * (1 - (decimal)Discount / (decimal)100), 2);

        public virtual Customer Customer { get; set; }

        public virtual Order Order { get; set; }

        public virtual ICollection<InvoicePosition> Positions { get; set; }
    }
}