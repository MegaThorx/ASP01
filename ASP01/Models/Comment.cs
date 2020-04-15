using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ASP01.Models
{
    [Table("Comments", Schema = "cust")]
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public int? ParentId { get; set; }

        [Display(Name = "Benutzer")]
        public string UserId { get; set; }
        
        [Display(Name = "Produkt")]
        public int ProductId { get; set; }
        
        public DateTime CreatedAt { get; set; }

        [StringLength(200)]
        public string Content { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Product Product { get; set; }

        public virtual Comment Parent { get; set; }
    }
}