using DB.IRepository;
using DB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bloggie.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IBlogRepository blogRepository;
        private readonly ITagRepository tagRepository;
        public List<BlogModel> Blogs = new List<BlogModel>();
        public List<Tag> Tags = new List<Tag>();

        public IndexModel(ILogger<IndexModel> logger, IBlogRepository blogRepository, ITagRepository tagRepository)
        {
            _logger = logger;
            this.blogRepository = blogRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<IActionResult> OnGet()
        {
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
            
            return Page();
        }
    }
}
