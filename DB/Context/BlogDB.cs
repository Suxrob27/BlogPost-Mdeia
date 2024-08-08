using DB.Model;
using DB.Model.BlogPostLike;
using Microsoft.EntityFrameworkCore;

namespace DB.Context
{
    public class BlogDB : DbContext
    {
        public BlogDB(DbContextOptions<BlogDB> options) : base(options)
        {

        }
        public DbSet<BlogModel> blogModel { get; set; }
        public DbSet<Tag> tags { get; set; }
        public DbSet<BlogPostLikeViewModel> blogPostLikes { get; set; }
        public DbSet<BlogPostCommentViewModel> blogPostComments { get; set; }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogModel>().HasKey(e => e.Id);
        }
    }

}

