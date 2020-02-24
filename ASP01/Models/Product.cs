﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASP01.Models
{
    [Table("Product", Schema = "cust")]
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [StringLength(40)]
        [Display(Name = "Bezeichnung")]
        public string Name { get; set; }

        [StringLength(200)]
        [Display(Name = "Beschreibung")]
        public string Description { get; set; }
        
        [Display(Name = "Preis")]
        public decimal Price { get; set; }
    }
}