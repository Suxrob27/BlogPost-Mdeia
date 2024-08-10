using DB.Context;
using DB.IRepository;
using DB.Model;
using DB.Model.Notification;
using DB.Model.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bloggie.Web.Pages.Admin.NewFolder.Blog
{
    [Authorize(Policy = "superAdmin")]
    public class AddModel : PageModel
     {
        private readonly BlogDB _db;
        private readonly IBlogRepository blogRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public AddViewModel blogModel { get; set; }
        [BindProperty]
        public IFormFile FeaturedFile { get; set; }

        [BindProperty]
        public string Tags { get; set; }
        public AddModel(BlogDB db, IBlogRepository blogRepository, UserManager<ApplicationUser> userManager)
        {
            _db = db;
            this.blogRepository = blogRepository;
            _userManager = userManager;
        }

        public async void OnGet()
        {
          
        }
      
        public async Task<IActionResult> OnPost() 
        {
            if (ModelState.IsValid)
            {

                var blogpost = new BlogModel()
                {
                    Heading = blogModel.Heading,
                    PageTitle = blogModel.PageTitle,
                    Content = blogModel.Content,
                    ShortDescription = blogModel.ShortDescription,
                    FeaturedImageUrl = blogModel.FeaturedImageUrl,
                    UrlHandle = blogModel.UrlHandle,
                    PublishedDate = blogModel.PublishedDate,
                    Author = blogModel.Author,
                    Visible = blogModel.Visible,
                    Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim() }))

                };

                await blogRepository.AddAsync(blogpost);
                var notification = new NotificationModel
                {
                    Message = "New Blog Post Was Successfully Added",
                    Type = NotificationType.Success,
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);
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
