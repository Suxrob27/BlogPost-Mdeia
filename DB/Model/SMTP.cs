using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Model
{
    public class SMTP
    {
        public string Server { get; set; }  
        public string Login { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
    }

}
