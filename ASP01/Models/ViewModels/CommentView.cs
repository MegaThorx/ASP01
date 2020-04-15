using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ASP01.Models.ViewModels
{
    public class CommentView
    {
        [Display(Name = "Übergeordnetes Kommentar")]
        public int? ParentId { get; set; }

        [Display(Name = "Produkt")]
        public int ProductId { get; set; }

        [StringLength(200)]
        [Display(Name = "Inhalt")]
        public string Content { get; set; }
    }
}