using DB.Context;
using DB.IRepository;
using DB.Model;
using DB.Model.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

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
        public async Task<IActionResult> OnPostEdit()
        {
            if (ModelState.IsValid && blogModel != null)
            {
              await _blogRepository.UpdateAsync(blogModel);

                ViewData["Notification"] = new NotificationModel
                {
                    Message = "The Post Was Successfully Edited",
                    Type = NotificationType.Success,
                };

                return RedirectToPage("/admin/blog/edit");
            }
            else
            {

                ViewData["Notification"] = new NotificationModel
                {
                    Message = "Occured Some Error, Try it Again or Later",
                    Type = NotificationType.Error,
                };
                return Page();
            }
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var existingblog = await _blogRepository.GetAsync(blogModel.Id) ;

               
            if (existingblog != null)
            {
                await _blogRepository.DeleteAsync(existingblog.Id);
                var notification = new NotificationModel
                {
                    Message = "The Post Was Successfully Deleted",
                    Type = NotificationType.Info,
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return RedirectToPage("/Admin/Blog/BlogPostList");
            }
            else
            {
                var notification = new NotificationModel
                {
                    Message = "Some Error Happened in Deleting. Try it Again Or Later",
                    Type = NotificationType.Error,
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);
                return Page();
            }
        }
    }
}
