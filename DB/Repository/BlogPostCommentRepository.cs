using DB.Context;
using DB.IRepository;
using DB.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Repository
{
    public class BlogPostCommentRepository : IBlogPostCommentRepository
    {
        private readonly BlogDB dB;

        public BlogPostCommentRepository(BlogDB dB)
        {
            this.dB = dB;
        }
        public async Task<BlogPostCommentViewModel> AddAsync(BlogPostCommentViewModel blogPostCommentView)
        {
             await dB.blogPostComments.AddAsync(blogPostCommentView);
            await dB.SaveChangesAsync();
            return blogPostCommentView; 
        }

        public  BlogPostCommentViewModel Delete(BlogPostCommentViewModel blogPostCommentView)
        {
            dB.blogPostComments.Remove(blogPostCommentView);  
            dB.SaveChanges();
            return blogPostCommentView; 
        }

        public async Task<IEnumerable<BlogPostCommentViewModel>> GetAllAsync(Guid blogPostId)
        {
            return  await dB.blogPostComments.Where(x => x.BlogPostId == blogPostId).ToListAsync();

        }
    }
}
