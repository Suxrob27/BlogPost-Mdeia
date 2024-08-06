using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Model.BlogPostLike
{
    public class BlogPostLikeViewModel
    {
        public Guid Id { get; set; }        
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }    
    }
}
