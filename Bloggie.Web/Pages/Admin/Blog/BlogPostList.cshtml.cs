using DB.Context;
using DB.IRepository;
using DB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Pages.Admin.Blog
{
    public class BlogPostListModel : PageModel
    {
        private readonly BlogDB _dB;
        private readonly IBlogRepository _blogRepository;
        public List<BlogModel> blogList;  
        public BlogPostListModel(BlogDB dB, IBlogRepository blogRepository)
        {
            _dB = dB;
           _blogRepository = blogRepository;
        }
        public async Task OnGet()
        {
           await _blogRepository.GetAllPosts();
        }
    }
}
