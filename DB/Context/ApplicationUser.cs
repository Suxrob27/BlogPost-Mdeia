using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Context
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        [NotMapped]
        public string RoleId { get; set; }
        [NotMapped]
        public string Role { get; set; }
        [NotMapped]
        public string UserClaim { get; set; }
    }
}
