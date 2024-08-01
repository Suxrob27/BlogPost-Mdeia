using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Model.TwoFactorAuth
{
    public class TFAAuthenticationViewModel
    {
        public string Code { get; set; }
        public string? Token { get; set; }
        public string? QrCodeUrl { get; set; } 
    }
}
