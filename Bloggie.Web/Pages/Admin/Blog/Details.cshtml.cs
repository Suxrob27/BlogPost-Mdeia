using DB.Context;
using DB.IRepository;
using DB.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security;

namespace Bloggie.Web.Pages.Admin.Blog
{
    [Authorize]
    public class DetailModel : PageModel
    {

        private readonly IBlogRepository blogRepository;
        private readonly IBlogPostLikeRepository blogPostLikes;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly BlogDB _db;

        public BlogModel BlogModel { get; set; }
        public Task<int> TotalLikes {  get; set; }
        public bool Liked { get; set; }    
        public DetailModel(IBlogRepository blogRepository, IBlogPostLikeRepository blogPostLikes, SignInManager<ApplicationUser> signInManager, 
            UserManager<ApplicationUser> userManager , BlogDB blogDB)
        {
            this.blogRepository = blogRepository;
            this.blogPostLikes = blogPostLikes;
            this.signInManager = signInManager;
            this.userManager = userManager;
            _db = blogDB;
        }
        public async Task<IActionResult> OnGet(string urlHandle)
        {
            BlogModel = await blogRepository.GetAsync(urlHandle);
            if (BlogModel != null) 
            {
                if (signInManager.IsSignedIn(User))
                {
                    var likes = await _db.blogPostLikes.Where(x => x.BlogPostId == BlogModel.Id).ToListAsync();
                   
                    var userId =  userManager.GetUserId(User);

                    Liked = likes.Any(x =>x.UserId == Guid.Parse(userId));
                    }   
                }

            TotalLikes = blogPostLikes.GetLikesCount(BlogModel.Id);

            return Page();
            }
          
        }
    }

