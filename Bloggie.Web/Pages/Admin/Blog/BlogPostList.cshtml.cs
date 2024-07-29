using DB.Context;
using DB.IRepository;
using DB.Model;
using DB.Model.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bloggie.Web.Pages.Admin.Blog
{
    public class BlogPostListModel : PageModel
    {
        private readonly BlogDB _dB;
        private readonly IBlogRepository _blogRepository;
        public List<BlogModel> blogList;  
        public BlogPostListModel(BlogDB dB, IBlogRepository blogRepository)
        {
            _dB = dB;
           _blogRepository = blogRepository;
        }
        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] =  JsonSerializer.Deserialize<NotificationModel>(notificationJson);
            }
            blogList = (await _blogRepository.GetAllPosts())?.ToList(); 
        }
    }
}
