using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DB.Model
{
    public class BlogComment
    {
        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string Description { get; set; }
        public DateTime CreatedData { get; set; }
        public string UserName { get; set; }
    }
}
