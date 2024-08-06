using DB.Model;
using DB.Model.BlogPostLike;
using DB.Model.BlogPostLike;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.IRepository
{
    public interface IBlogPostLikeRepository
    {
        public Task<int>  GetLikesCount(Guid blogPostId);
        public Task AddLikeforBlog(AddBlogPostLikeRequest model);
    }
}
