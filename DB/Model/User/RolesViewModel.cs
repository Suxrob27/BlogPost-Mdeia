using DB.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Model.User
{
    public class RolesViewModel
    {
        public RolesViewModel()
        {
            RoleList = [];
        }
        public ApplicationUser User { get; set; }
        public List<RoleSelection> RoleList { get; set; }
    }

    public class RoleSelection
    {
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }    
    }
}
