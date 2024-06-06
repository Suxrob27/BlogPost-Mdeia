using DB.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Context
{
     public class BlogDB : DbContext
    {
        public BlogDB(DbContextOptions<BlogDB> options) : base(options)
        {
                
        }
        public DbSet<BlogModel> blogModel { get; set; }
        public DbSet<Tag>  tags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogModel>().HasMany(x => x.Tags).WithOne(x => x.Blog).HasForeignKey(x => x.BlogPostId);
        }
    }
}
