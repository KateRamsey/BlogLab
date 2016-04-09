using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;

namespace BlogLab.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        //TODO: Find way to validate that this is a link using an attribute
        public string ImageLink { get; set; }

        public DateTime CreateDate { get; set; }
    }
}