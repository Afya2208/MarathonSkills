using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarathonSkills.Database
{
    public partial class User
    {
        public string RoleName
        {
            get
            {
                Role role = DB.entities.Role.FirstOrDefault(r => r.RoleId == RoleId);
                return role?.RoleName;
            }
        }
    }
}
