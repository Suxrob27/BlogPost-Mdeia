using DB.Model.BlogPostLike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Model
{
    public class BlogModel
    {
        public Guid Id { get; set; }
        public string Heading { get; set; } = string.Empty;
        public string PageTitle { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string FeaturedImageUrl { get; set; } = string.Empty;
        public string UrlHandle { get; set; } = string.Empty;
        public DateTime? PublishedDate { get; set; }
        public string? Author { get; set; }
        public bool Visible { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<BlogPostLikeViewModel> Likes { get; set; } = new List<BlogPostLikeViewModel>();   
        public ICollection<BlogPostCommentViewModel> Comments { get;set; }
    }
}
