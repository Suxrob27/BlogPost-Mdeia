using DB.Context;
using DB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.NewFolder.Blog
{
    public class AddModel : PageModel
     {
        private readonly BlogDB _db;

        [BindProperty]
        public BlogModel blogModel { get; set; }
        public AddModel(BlogDB db)
        {
            _db = db;
        }
        [HttpGet]
        public void OnGet()
        {
           
        }
        [HttpPost]
        public async Task<IActionResult> OnPostAsync() 
        {
            if (ModelState.IsValid && blogModel != null)
            {
               await _db.blogModel.AddAsync(blogModel);
                await _db.SaveChangesAsync();
               return RedirectToPage("/admin/blog/BlogPostList");
            }
            else
            {
                return Page();
            }
        }
    }
}
