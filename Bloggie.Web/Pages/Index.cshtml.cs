using DB.Context;
using DB.IRepository;
using DB.Model;
using DB.Model.Notification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace Bloggie.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlogRepository blogRepository;
        private readonly ITagRepository tagRepository;
        private readonly UserManager<ApplicationUser> userManager;
        public List<BlogModel> Blogs = new List<BlogModel>();
        public List<Tag> Tags = new List<Tag>();

        public IndexModel(ILogger<IndexModel> logger, IBlogRepository blogRepository, ITagRepository tagRepository, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            this.blogRepository = blogRepository;
            this.tagRepository = tagRepository;
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var notification = (string)TempData["Notification"];
            if (notification != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<NotificationModel>(notification); 
            }
            Blogs = (await blogRepository.GetAllPosts()).ToList();
            foreach (var tags in Blogs)
            {
                if (tags.Tags != null )
                {
                    foreach (var tag in tags.Tags)
                    {
                        Tags.Add(tag);
                    }
                }
            }
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                ViewData["2FactorAuth"] = false;    
            }
            else
            {
                ViewData["2FactorAuth"] = user.TwoFactorEnabled;
            }

            return Page();
        }
    }
}
