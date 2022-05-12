using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itmo.Dormitory.DataAccess
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }

    public class AppRole : IdentityRole { }

}
