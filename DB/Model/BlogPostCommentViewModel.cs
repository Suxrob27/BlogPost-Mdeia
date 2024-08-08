using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Model
{
    public class BlogPostCommentViewModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }    
    }
}
