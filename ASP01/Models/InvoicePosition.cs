using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASP01.Models
{
    [Table("InvoicePositions", Schema = "cust")]
    public class InvoicePosition
    {

        [Key, Column(Order = 0)]
        public int InvoiceId { get; set; }

        [Key, Column(Order = 1)]
        [Display(Name = "Position")]
        public int Position { get; set; }

        [Display(Name = "Produkt")]
        [Required]
        public int? ProductId { get; set; }
        
        [StringLength(40)]
        [Display(Name = "Bezeichnung")]
        public string ProductName { get; set; }

        [StringLength(200)]
        [Display(Name = "Beschreibung")]
        public string ProductDescription { get; set; }

        [Display(Name = "Preis")]
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Display(Name = "Menge")]
        [Required]
        public int Amount { get; set; }

        [Display(Name = "Summe")]
        public decimal Sum => (Amount * Price) * (1 - (decimal)Discount / (decimal)100);

        [Display(Name = "Rabatt")]
        [Range(0.0f, 100.0f)]
        [Required]
        public float Discount { get; set; }

        public virtual Invoice Invoice { get; set; }

        public virtual Product Product { get; set; }
    }
}