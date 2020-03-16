using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASP01.Models
{
    [Table("Orders", Schema = "cust")]
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Display(Name = "Bestelldatum")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Kunde")]
        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "Rabatt")]
        [Range(0.0f, 100.0f)]
        [Required]
        public float Discount { get; set; }

        [Display(Name = "Total")]
        public decimal Total => (Positions.Sum(p => p.Sum)) * (1 - (decimal)Discount / (decimal)100);

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderPosition> Positions { get; set; }
    }
}