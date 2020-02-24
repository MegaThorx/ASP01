using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP01.Models.ViewModels
{
    public class CustomerNamesView
    {
        [Key]
        public int CustomerId { get; set; }

        [Display(Name = "Name")]
        public string FullName { get; set; }
    }
}