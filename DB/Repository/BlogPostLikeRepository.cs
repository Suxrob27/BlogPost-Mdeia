using DB.Context;
using DB.IRepository;
using DB.Model;
using DB.Model.BlogPostLike;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Repository
{
    public class BlogPostLikeRepository : IBlogPostLikeRepository
    {
        private readonly BlogDB db;

        public BlogPostLikeRepository(BlogDB db)
        {
            this.db = db;
        }

        public async Task AddLikeforBlog(AddBlogPostLikeRequest model)
        {
            var like = new BlogPostLikeViewModel()
            {
                Id = new Guid(),
                UserId = model.userId,
                BlogPostId = model.blogPostId
            };

           await db.blogPostLikes.AddAsync(like);
           await db.SaveChangesAsync();

        }

        public async Task<IEnumerable<BlogPostLikeViewModel>> GetLikeforblog(Guid blogPostId)
        {
            return   await db.blogPostLikes.Where(x => x.BlogPostId == blogPostId).ToListAsync();

        }

        public async Task<int> GetLikesCount(Guid blogPostId)
        {
            return await db.blogPostLikes
                           .CountAsync(x => x.BlogPostId == blogPostId);
        }
    }
}

