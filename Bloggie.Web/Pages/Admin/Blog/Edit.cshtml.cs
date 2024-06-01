using DB.Context;
using DB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages.Admin.Blog
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public BlogModel blogModel { get; set; }
        private readonly BlogDB _dB;

        public EditModel(BlogDB dB)
        {
            _dB = dB;
        }
        public void OnGet(Guid id)
        {
            blogModel = _dB.blogModel.FirstOrDefault(x => x.Id == id)?? new BlogModel();

        }
        [HttpPost]
        public async Task<IActionResult> OnPostEditAsync()
        {
            if (ModelState.IsValid && blogModel != null)
            {
                 _dB.blogModel.Update(blogModel);
                await _dB.SaveChangesAsync();
                return RedirectToPage("/admin/blog/edit");
            }
            else
            {
                return Page();
            }
        }

        public IActionResult OnPostDelete()
        {
            var existingblog = _dB.blogModel.FirstOrDefault(x => x.Id == blogModel.Id);
            if (existingblog != null)
            {
                _dB.Remove(existingblog);
                _dB.SaveChanges();
                return RedirectToPage("/Admin/Blog/BlogPostList");
            }

                return Page();
            
        }
    }
}
