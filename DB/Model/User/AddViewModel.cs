using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Model.User
{
     public class AddViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Heading { get; set; } = string.Empty;
        [Required]
        public string PageTitle { get; set; } = string.Empty;
        [Required]
        [MaxLength(1000)]
        [MinLength(1)]
        public string Content { get; set; } = string.Empty;
        [Required]
        public string ShortDescription { get; set; } = string.Empty;
        public string FeaturedImageUrl { get; set; } = string.Empty;
        [Required]
        public string UrlHandle { get; set; } = string.Empty;
        public DateTime? PublishedDate { get; set; }
        public string? Author { get; set; }
        [Required]
        public bool Visible { get; set; }
    }
}
