using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Model.TwoFactorAuth
{
    public class VerifyAuthenticatiorviewModel
    {
        [Required]
        public string Code { get; set; }
        public string ReturnUUrl { get; set; }
        [Display(Name ="Remember me?")]
        public bool RememberMe { get; set; }
    }
}
