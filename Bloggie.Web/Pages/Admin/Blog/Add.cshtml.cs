using DB.Context;
using DB.IRepository;
using DB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.NewFolder.Blog
{
    public class AddModel : PageModel
     {
        private readonly BlogDB _db;
        private readonly IBlogRepository blogRepository;

        [BindProperty]
        public BlogModel blogModel { get; set; }
        public AddModel(BlogDB db, IBlogRepository blogRepository)
        {
            _db = db;
            this.blogRepository = blogRepository;
        }

        public void OnGet()
        {
           
        }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync() 
        {
            if (ModelState.IsValid && blogModel != null)
            {
              await blogRepository.AddAsync(blogModel);
               return RedirectToPage("/admin/blog/BlogPostList");
            }
            else
            {
                return Page();
            }
        }
    }
}
