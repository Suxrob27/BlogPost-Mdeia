using DB.Context;
using DB.IRepository;
using DB.Model;
using DB.Model.Notification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using DB.Model.BlogFunc;

namespace Bloggie.Web.Pages.Admin.Blog
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IBlogRepository blogPostRepository;

        [BindProperty]
        public EditBlogPostRequest BlogPost { get; set; }

        [BindProperty]
        public IFormFile FeaturedImage { get; set; }

        [BindProperty]
        [Required]
        public string Tags { get; set; }

        public EditModel(IBlogRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public async Task OnGet(Guid id)
        {
            var blogPostDomainModel = await blogPostRepository.GetAsync(id);

            if (ModelState.IsValid)
            {
                BlogPost = new EditBlogPostRequest
                {
                    Id = blogPostDomainModel.Id,
                    Heading = blogPostDomainModel.Heading,
                    PageTitle = blogPostDomainModel.PageTitle,
                    Content = blogPostDomainModel.Content,
                    ShortDescription = blogPostDomainModel.ShortDescription,
                    FeaturedImageUrl = blogPostDomainModel.FeaturedImageUrl,
                    UrlHandle = blogPostDomainModel.UrlHandle,
                    PublishedDate = blogPostDomainModel.PublishedDate,
                    Author = blogPostDomainModel.Author,
                    Visible = blogPostDomainModel.Visible
                };

                Tags = string.Join(',', blogPostDomainModel.Tags.Select(x => x.Name));
            }
        }

        public async Task<IActionResult> OnPostEdit()
        {
            ValidateEditBlogPost();

            if (ModelState.IsValid)
            {
                try
                {
                    var blogPostDomainModel = new BlogModel
                    {
                        Id = BlogPost.Id,
                        Heading = BlogPost.Heading,
                        PageTitle = BlogPost.PageTitle,
                        Content = BlogPost.Content,
                        ShortDescription = BlogPost.ShortDescription,
                        FeaturedImageUrl = BlogPost.FeaturedImageUrl,
                        UrlHandle = BlogPost.UrlHandle,
                        PublishedDate = BlogPost.PublishedDate,
                        Author = BlogPost.Author,
                        Visible = BlogPost.Visible,
                        Tags = new List<Tag>(Tags.Split(',').Select(x => new Tag() { Name = x.Trim() }))
                    };


                    await blogPostRepository.UpdateAsync(blogPostDomainModel);

                    ViewData["Notification"] = new NotificationModel
                    {
                        Type = NotificationType.Success,
                        Message = "Record updated successfully!"
                    };
                }
                catch (Exception ex)
                {
                    ViewData["Notification"] = new NotificationModel
                    {
                        Type = NotificationType.Error,
                        Message = "Something went wrong!"
                    };
                }

                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await blogPostRepository.DeleteAsync(BlogPost.Id);
            if (deleted)
            {
                var notification = new NotificationModel
                {
                    Type = NotificationType.Success,
                    Message = "Blog was deleted successfully!"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/Admin/Blogs/List");
            }

            return Page();
        }


        private void ValidateEditBlogPost()
        {
            if (!string.IsNullOrWhiteSpace(BlogPost.Heading))
            {
                // check for minimum length
                if (BlogPost.Heading.Length < 10 || BlogPost.Heading.Length > 72)
                {
                    ModelState.AddModelError("BlogPost.Heading",
                        "Heading can only be between 10 and 72 characters.");
                }
                // check for maximum length
            }
        }
    }
}
