using DB.IRepository;
using DB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Tags
{
    public class DetailsModel : PageModel
    {
        private readonly IBlogRepository blogRepository;
        public List<BlogModel> BlogModel { get; set; }   

        public DetailsModel(IBlogRepository blogRepository)
        {
            this.blogRepository = blogRepository;
        }
        public async Task<IActionResult> OnGet(string tagName)
        {
            BlogModel = (await blogRepository.GetAllAsync(tagName)).ToList(); 
            return Page();
        }
    }
}
