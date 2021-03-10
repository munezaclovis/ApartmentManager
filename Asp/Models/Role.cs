using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Models
{
    public class Role : IdentityRole
    {
        public Role(string name) : base(name) { }
        public ICollection<UserRole> UsersRoles { get; set; }
    }
}
