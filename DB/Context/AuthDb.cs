using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Context
{
    public class AuthDb : IdentityDbContext
    {
        public AuthDb(DbContextOptions<AuthDb> options) : base(options)
        {
                
        }
    }
}
