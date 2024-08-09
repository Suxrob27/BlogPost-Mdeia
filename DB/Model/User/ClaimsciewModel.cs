using DB.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Model.User
{
    public class ClaimsciewModel
    {
        public ClaimsciewModel()
        {
            ClaimList = [];
        }
        public ApplicationUser User { get; set; }
        
        public List<ClaimSelection> ClaimList { get; set; } 
    }

    public class ClaimSelection
    {
        public string ClaimType { get; set; }
        public bool IsSelected { get; set; }    
    }
}
