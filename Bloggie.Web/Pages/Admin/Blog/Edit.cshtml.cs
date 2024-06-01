using DB.Context;
using DB.IRepository;
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
        private readonly IBlogRepository _blogRepository;

        public EditModel(BlogDB dB, IBlogRepository blogRepository)
        {
            _dB = dB;
            _blogRepository = blogRepository;
        }
        public async Task  OnGet(Guid id)
        {
            blogModel = await _blogRepository.GetAsync(id) ?? new BlogModel();

        }
        [HttpPost]
        public async Task<IActionResult> OnPostEditAsync()
        {
            if (ModelState.IsValid && blogModel != null)
            {
              await _blogRepository.UpdateAsync(blogModel);
                return RedirectToPage("/admin/blog/edit");
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var existingblog = await _blogRepository.GetAsync(blogModel.Id) ;
            if (existingblog != null)
            {
              await _blogRepository.DeleteAsync(existingblog.Id);
                return RedirectToPage("/Admin/Blog/BlogPostList");
            }

                return Page();
            
        }
    }
}
