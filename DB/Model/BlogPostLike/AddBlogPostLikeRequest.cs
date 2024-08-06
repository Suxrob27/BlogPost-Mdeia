using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Model.BlogPostLike
{
    public class AddBlogPostLikeRequest
    {
     public Guid  userId { get; set; }  
     public Guid blogPostId { get; set; }   
    }
}
