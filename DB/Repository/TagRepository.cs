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
   public  class TagRepository : ITagRepository
    {
        private readonly BlogDB _db;

        public TagRepository(BlogDB _db)
        {
            this._db = _db;
        }
        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            var tags = await _db.tags.ToListAsync();
            return tags.DistinctBy(x => x.Name.ToLower());

        }
    }
}
