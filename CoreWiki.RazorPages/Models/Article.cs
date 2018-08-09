using NodaTime;
using System;
using System.ComponentModel.DataAnnotations;
using NodaTime.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreWiki.RazorPages.Models
{
    public class Article
    {
        [Required, Key, MaxLength(100)]
        public string Topic { get; set; }
        
        [NotMapped]
        public Instant Published { get; set; }

        [Obsolete("This property only exists for EF-serialization purposes")]
        [DataType(DataType.DateTime)]
        [Column("Published")]
        public DateTime PublishedDateTime
        {
            get => Published.ToDateTimeUtc();
            set => Published = DateTime.SpecifyKind(value, DateTimeKind.Utc).ToInstant();
        }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}
