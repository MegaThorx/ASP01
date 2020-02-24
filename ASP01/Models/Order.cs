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
        public DateTime OrderDate { get; set; }

        [Display(Name = "Kunde")]
        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "Rabatt")]
        [Range(0.0f, 100.0f)]
        [Required]
        public float Discount { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<OrderPosition> Positions { get; set; }
    }
}