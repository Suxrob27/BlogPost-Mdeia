using DB.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageAPI : ControllerBase
    {
        private readonly IImageRepostiory imageRepostiory;

        public ImageAPI(IImageRepostiory imageRepostiory)
        {
            this.imageRepostiory = imageRepostiory;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile file) 
        {
           var imageUrl = await imageRepostiory.UploadAsync(file);  
            if(imageUrl == null) 
            {
                return Problem("Smth Went Wrong MAn Try It Later Or Try It Now");
            }
            return Ok(new { link = imageUrl});
        }

    }
}
