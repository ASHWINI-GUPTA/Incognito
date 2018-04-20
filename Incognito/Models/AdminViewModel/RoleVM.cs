using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Incognito.Models.AdminViewModel
{
    public class RoleVM
    {
        public IQueryable<IdentityRole> IdentityRole { get; set; }

        public AddRoleVM AddRole { get; set; }
    }
}
