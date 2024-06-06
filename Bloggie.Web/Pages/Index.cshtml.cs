using DB.IRepository;
using DB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlogRepository blogRepository;

        public List<BlogModel> Blogs;

        public IndexModel(ILogger<IndexModel> logger, IBlogRepository blogRepository)
        {
            _logger = logger;
            this.blogRepository = blogRepository;
        }

        public async Task<IActionResult> OnGet()
        {
            Blogs = (await blogRepository.GetAllPosts()).ToList();
            return Page();
        }
    }
}
