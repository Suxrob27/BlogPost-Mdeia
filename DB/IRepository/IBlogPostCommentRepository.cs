using DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.IRepository
{
    public interface IBlogPostCommentRepository
    {
        Task<BlogPostCommentViewModel> AddAsync(BlogPostCommentViewModel blogPostCommentView);
        Task<IEnumerable<BlogPostCommentViewModel>> GetAllAsync(Guid blogPostId);
    }
}
