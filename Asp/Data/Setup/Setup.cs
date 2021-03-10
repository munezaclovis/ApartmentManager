using Asp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Data.Setup
{
    public static class Setup
    {
        public static void RunAppDefaultAsync(IServiceProvider serviceProvider)
        {
            RoleManager<Role> roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            foreach (string roleName in DefaultData.DefaultRoles)
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    roleManager.CreateAsync(new Role(roleName)).Wait();
                }
            }
        }
    }
}