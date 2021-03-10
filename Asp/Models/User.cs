using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string Firstname { set; get; }

        [Required]
        public string Lastname { set; get; }

        [Required]
        [DefaultValue("A")]
        public string Status { set; get; } = "A";
        
        public ICollection<UserRole> UsersRoles { get; set; }
        public ICollection<Building> Buildings { get; set; }


        public override string ToString()
        {
            return this.Id + ", " + this.Email + ", " + this.Firstname + ", " + Lastname + ", " + Status + ", " + UserName;
        }
    }
}
