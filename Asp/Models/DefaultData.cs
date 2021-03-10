using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Models
{
    public static class DefaultData
    {
        public static List<string> DefaultRoles = new List<string> { "Administrator", "Manager", "Tenant" };
        public static Dictionary<string, string> DefaultDataStatus = new Dictionary<string, string> { { "A", "Active" }, { "I", "Inactive" }, { "D", "Deleted" }, { "P", "Pending" } };
    }
}
