using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP01.Models.ViewModels
{
    /// <summary>
    /// Customer ViewModel to edit first- and lastname
    /// </summary>
    public class CustomerEditView
    {
        public int CustomerId { get; set; }

        [Display(Name = "Vorname")]
        [StringLength(100)]
        public string FName { get; set; }

        [Display(Name = "Nachname")]
        [StringLength(100, MinimumLength = 1)]
        [Required]
        public string LName { get; set; }
    }
}