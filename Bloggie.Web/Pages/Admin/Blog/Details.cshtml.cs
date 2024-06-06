using DB.IRepository;
using DB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.Blog
{
    public class DetailModel : PageModel
    {
        private readonly IBlogRepository blogRepository;
        public BlogModel BlogModel;
        public DetailModel(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }
        public async Task<IActionResult> OnGet(string urlHandle )
        {
            BlogModel = await blogRepository.GetAsync(urlHandle);   
            return Page();  
        }
    }
}
