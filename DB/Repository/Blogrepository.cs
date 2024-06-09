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
            db.SaveChanges();
            return true;


        }

        public async Task<IEnumerable<BlogModel>> GetAllAsync(string tagName)
        {
          return  await db.blogModel.Where(x => x.Tags.Any(x => x.Name == tagName)).Include(nameof(BlogModel.Tags)).ToListAsync();   
        }

        public async Task<IEnumerable<BlogModel>> GetAllPosts()
        {
            return await db.blogModel.ToListAsync();
        }

        public async Task<BlogModel> GetAsync(Guid id)
        {
            return await db.blogModel.Include(nameof(BlogModel.Tags)).FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<BlogModel> GetAsync(string urlHnadler)
        {
            return db.blogModel.Include(nameof(BlogModel.Tags)).FirstOrDefaultAsync(x => x.UrlHandle == urlHnadler);
        }

        public async Task<BlogModel> UpdateAsync(BlogModel blogPost)
        {
            var existingBlogPost = await db.blogModel.Include(nameof(BlogModel.Tags)).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
            if (existingBlogPost != null)
            {
                existingBlogPost.Heading = blogPost.Heading;
                existingBlogPost.PageTitle = blogPost.PageTitle;
                existingBlogPost.Content = blogPost.Content;
                existingBlogPost.ShortDescription = blogPost.ShortDescription;
                existingBlogPost.FeaturedImageUrl = blogPost.FeaturedImageUrl;
                existingBlogPost.UrlHandle = blogPost.UrlHandle;
                existingBlogPost.PublishedDate = blogPost.PublishedDate;
                existingBlogPost.Author = blogPost.Author;
                existingBlogPost.Visible = blogPost.Visible;
                existingBlogPost.Tags = blogPost.Tags;

                if(blogPost.Tags != null && blogPost.Tags.Any())
                {
                    //Delete the Exisiting tags
                    db.tags.RemoveRange(existingBlogPost.Tags);

                    //Add new tags
                    blogPost.Tags.ToList().ForEach(x => x.BlogPostId = existingBlogPost.Id);
                    await db.tags.AddRangeAsync(blogPost.Tags);

                }


                await db.SaveChangesAsync();

                return blogPost;

            }
            return null;
        }
    }
}
