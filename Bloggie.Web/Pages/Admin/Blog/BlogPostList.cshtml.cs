using DB.Context;
using DB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.Blog
{
    public class BlogPostListModel : PageModel
    {
        private readonly BlogDB _dB;
        public List<BlogModel> blogList;  
        public BlogPostListModel(BlogDB dB)
        {
            _dB = dB;
        }
        public void OnGet()
        {
            blogList = _dB.blogModel.ToList();
        }
    }
}
