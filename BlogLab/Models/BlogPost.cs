using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogLab.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Author { get; set; }
        public DateTime CreateDate { get; set; }
    }
}