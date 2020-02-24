using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASP01.Models
{
    [Table("OrderPositions", Schema = "cust")]
    public class OrderPosition
    {

        [Key, Column(Order = 0)]
        public int OrderId { get; set; }

        [Key, Column(Order = 1)]
        [Display(Name = "Position")]
        public int Position { get; set; }

        [Display(Name = "Produkt")]
        [Required]
        public int ProductId { get; set; }
        
        [Display(Name = "Menge")]
        [Required]
        public int Amount { get; set; }

        [Display(Name = "Preis")]
        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Display(Name = "Rabatt")]
        [Range(0.0f, 100.0f)]
        [Required]
        public float Discount { get; set; }

        public virtual Product Product { get; set; }

        public virtual Order Order { get; set; }
    }
}