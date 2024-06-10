using DB.Context;
using DB.IRepository;
using DB.Model;
using DB.Model.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bloggie.Web.Pages.Admin.NewFolder.Blog
{
    public class AddModel : PageModel
     {
        private readonly BlogDB _db;
        private readonly IBlogRepository blogRepository;

        [BindProperty]
        public BlogModel blogModel { get; set; }
        [BindProperty]
        public IFormFile FeaturedFile { get; set; }

        [BindProperty]
        public string Tags { get; set; }
        public AddModel(BlogDB db, IBlogRepository blogRepository)
        {
            _db = db;
            this.blogRepository = blogRepository;
        }

        public void OnGet()
        {

        }
      
        public async Task<IActionResult> OnPost() 
        {
            blogModel.Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag { Name = x.Trim() }));

            if (blogModel != null)
            {
                await blogRepository.AddAsync(blogModel);
                var notification  = new NotificationModel
                {
                    Message = "New Blog Post Was Successfully Added",
                    Type = NotificationType.Success,
                };

               TempData["Notification"] =  JsonSerializer.Serialize(notification);
               return RedirectToPage("/admin/blog/BlogPostList");
            }
            else
            {
               ViewData["Notification"] = new NotificationModel
                {
                    Message = "Some Error Happened. Try it Again or Try it Later",
                    Type = NotificationType.Error,
                };
                
          
                return Page();
            }
        }
    }
}