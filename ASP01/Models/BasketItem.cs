using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASP01.Models
{
    [Table("BasketItem", Schema = "cust")]
    public class BasketItem
    {
        [Key, Column(Order = 0)]
        public string UserId { get; set; }

        [Key, Column(Order = 1)]
        [Display(Name = "Produkt")]
        public int ProductId { get; set; }

        [Display(Name = "Menge")]
        [Required]
        public int Amount { get; set; }

        [Display(Name = "Summe")]
        public decimal Sum => Amount * Product?.Price ?? 0;

        public virtual ApplicationUser User { get; set; }

        public virtual Product Product { get; set; }
    }
}