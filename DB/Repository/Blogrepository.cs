using DB.Context;
using DB.IRepository;
using DB.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Repository
{
    public class BlogRepository : IBlogRepository
    {
        private readonly BlogDB db;

        public BlogRepository(BlogDB _db)
        {
            db = _db;
        }
        public async Task<BlogModel> AddAsync(BlogModel model)
        {
          await db.blogModel.AddAsync(model);
          await db.SaveChangesAsync();
            return model;   
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var exsistingElement = await db.blogModel.FindAsync(id);
                db.blogModel.Remove(exsistingElement);
                return true;    
            
             
        }

        public async Task<IEnumerable<BlogModel>> GetAllPosts()
        {
            return await db.blogModel.ToListAsync();
        }

        public async Task<BlogModel> GetAsync(Guid id)
        {
            return await db.blogModel.FindAsync(id);
        }

        public async Task<BlogModel> UpdateAsync(BlogModel model)
        {
           var updatingModel =  await db.blogModel.FindAsync(model.Id);
            db.blogModel.Update(updatingModel);
            await db.SaveChangesAsync();   
            return updatingModel;   
           
        }
    }
}
