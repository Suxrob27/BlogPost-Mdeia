using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.IRepository
{
    public interface IImageRepostiory
    {
        Task<string> UploadAsync(IFormFile formFile);
    }
}
