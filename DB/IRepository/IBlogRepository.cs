﻿using DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.IRepository
{
    public interface IBlogRepository
    {
        Task<IEnumerable<BlogModel>> GetAllPosts();
        Task<BlogModel> GetAsync(Guid id);
        Task<BlogModel> GetAsync(string urlHnadler);
        Task<IEnumerable<BlogModel>> GetAllAsync(string tagName);
        Task<BlogModel> AddAsync(BlogModel model);
        Task<BlogModel> UpdateAsync(BlogModel model);
        Task<bool> DeleteAsync(Guid id);


    }
}
