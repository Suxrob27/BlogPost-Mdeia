using DB.IRepository;
using DB.Model.BlogPostLike;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostLikeController : ControllerBase
    {
        private readonly IBlogPostLikeRepository blogPostLikeRepository;

        public BlogPostLikeController(IBlogPostLikeRepository blogPostLikeRepository)
        {
            this.blogPostLikeRepository = blogPostLikeRepository;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddLike([FromBody] AddBlogPostLikeRequest blogPostLike)
        {
            if (blogPostLike != null)
            {
                await blogPostLikeRepository.AddLikeforBlog(blogPostLike);
                return Ok();
            }
            return BadRequest(ModelState.ErrorCount);    

        }
        [HttpGet]

        [Route("{blogPostId:Guid}/totalLikes")]
        public async Task<IActionResult> GetTotalLikes([FromRoute] Guid blogPostId)
        {
            var totalLikes = await blogPostLikeRepository.GetLikesCount(blogPostId);

            return Ok(totalLikes);
        }

    }
}
