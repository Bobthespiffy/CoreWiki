using System;
using System.ComponentModel.DataAnnotations;

namespace CoreWiki.RazorPages.Models
{
    public class Article
    {
        [Required, Key]
        public string Topic { get; set; }

        public DateTime Published { get; set; } = DateTime.UtcNow;

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
