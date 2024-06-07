using DB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.IRepository
{
    public  interface ITagRepository
    {
        public Task<IEnumerable<Tag>> GetAllAsync();
    }
}
