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
    [Authorize()]
    public class DetailModel : PageModel
    {

        private readonly IBlogRepository blogRepository;
        private readonly IBlogPostLikeRepository blogPostLikes;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly BlogDB _db;
        private readonly IBlogPostCommentRepository blogPostComment;
        public  List<BlogComment> Comments { get; set; }
        [BindProperty]
        public BlogPostCommentViewModel model { get; set; } = new BlogPostCommentViewModel();

        public BlogModel BlogModel { get; set; }
        public Task<int> TotalLikes { get; set; }
        public bool Liked { get; set; }
        
        public DetailModel(IBlogRepository blogRepository, IBlogPostLikeRepository blogPostLikes, SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, BlogDB blogDB,IBlogPostCommentRepository blogPostComment)
        {
            this.blogRepository = blogRepository;
            this.blogPostLikes = blogPostLikes;
            this.signInManager = signInManager;
            this.userManager = userManager;
            _db = blogDB;
            this.blogPostComment = blogPostComment;
        }
        public async Task<IActionResult> OnGet(string urlHandle)
        {
            BlogModel = await blogRepository.GetAsync(urlHandle);
            if (BlogModel != null)
            {
                if (signInManager.IsSignedIn(User))
                {
                    var likes = await _db.blogPostLikes.Where(x => x.BlogPostId == BlogModel.Id).ToListAsync();

                    var userId = userManager.GetUserId(User);

                    Liked = likes.Any(x => x.UserId == Guid.Parse(userId));
                    await GetComments();
                  
                }
            }

            TotalLikes = blogPostLikes.GetLikesCount(BlogModel.Id);

            return Page();
        }
        public async Task<IActionResult> OnPost(string urlHandle)
        {
            BlogModel = await blogRepository.GetAsync(urlHandle);
            model.BlogPostId = BlogModel.Id;
            var user = await userManager.GetUserAsync(User);
            if (signInManager.IsSignedIn(User) && ModelState.IsValid)
            {
                var comment = new BlogPostCommentViewModel()
                {
                    BlogPostId = model.BlogPostId,
                    Description = model.Description,    
                    UserId = model.UserId,  
                };
                await blogPostComment.AddAsync(comment);  
            }

            return RedirectToPage("/Admin/Blog/Details", urlHandle);
        }

        private async Task GetComments()
        {
            if (_db.blogPostComments.Any())
            {
                model.BlogPostId = BlogModel.Id;
                var user = await userManager.GetUserAsync(User);
                
                model.UserId = Guid.Parse(await userManager.GetUserIdAsync(user));
                var blogPostComments = await blogPostComment.GetAllAsync(model.BlogPostId);

                var blogPostViewModel = new List<BlogComment>();
                foreach (var comment in blogPostComments)
                {
                    comment.UserId = model.UserId;
                    blogPostViewModel.Add(new BlogComment
                    {
                        CreatedData = DateTime.Now,
                        Description = comment.Description,
                        UserName = (await userManager.FindByIdAsync(comment.UserId.ToString())).UserName
                    });
                }
                Comments = blogPostViewModel;
            }

        }
    }
}

