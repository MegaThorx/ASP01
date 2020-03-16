using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP01.Models.ViewModels
{
    public class OrderEditView
    {
        public int OrderId { get; set; }

        [Display(Name = "Bestelldatum")]
        [Required]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Kunde")]
        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "Rabatt")]
        [Range(0.0f, 100.0f)]
        [Required]
        public float Discount { get; set; }

        public List<OrderPositionEditView> Positions;
    }

    public class OrderPositionEditView
    {
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
    }
}