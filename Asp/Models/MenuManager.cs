using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Models
{
    public static class MenuManager
    {
        public static List<MenuItem> getMainMenuList()
        {
            List<MenuItem> mainMenuList = new List<MenuItem>();
            mainMenuList.Add(new MenuItem("Home", null, new MenuLink("", "Home", "Index"), new string[] {"Guest", "Administrator", "Tenant", "Manager"}));
            mainMenuList.Add(new MenuItem("Appointments", null, new MenuLink("", "Appointments", "Index"), new string[] { "Administrator", "Manager", "Tenant" }));
            mainMenuList.Add(new MenuItem("Messages", null, new MenuLink("", "Messages", "Index"), new string[] { "Administrator", "Manager", "Tenant" }));
            mainMenuList.Add(new MenuItem("Admin Area", null, new MenuLink("Administrator", "Dashboard", "Index"), new string[] { "Administrator" }));
            mainMenuList.Add(new MenuItem("Manager Area", null, new MenuLink("Manager", "DashBoard", "Index"), new string[] { "Administrator", "Manager" }));
            return mainMenuList;
        }

        public static List<MenuItem> getAdministratorMainMenuList()
        {
            List<MenuItem> administratorMainMenuList = new List<MenuItem>();
            administratorMainMenuList.Add(new MenuItem("Dashboard", null, new MenuLink("Administrator", "Dashboard", "Index"), null, null,"fas fa-fw fa-tachometer-alt"));
            administratorMainMenuList.Add(new MenuItem("Users", new List<MenuItem> { new MenuItem("Add User", null, new MenuLink("Administrator", "Users", "Add"), null), new MenuItem("Manage Users", null, new MenuLink("Administrator", "Users", "Index"), null) }, new MenuLink("Administrator", "Users", "Index"), null, null, "fas fa-fw fa-users"));
            administratorMainMenuList.Add(new MenuItem("Buildings", new List<MenuItem> { new MenuItem("Add Building", null, new MenuLink("Administrator", "Buildings", "Add"), null), new MenuItem("Manage Buildings", null, new MenuLink("Administrator", "Buildings", "Index"), null) }, new MenuLink("Administrator", "Buildings", "Index"), null, null, "fas fa-fw fa-city"));
            administratorMainMenuList.Add(new MenuItem("Apartments", new List<MenuItem> { new MenuItem("Add Apartment", null, new MenuLink("Administrator", "Apartments", "Add"), null), new MenuItem("Manage Apartments", null, new MenuLink("Administrator", "Apartments", "Index"), null) }, new MenuLink("Administrator", "Apartments", "Index"), null, null, "fas fa-fw fa-door-open"));
            administratorMainMenuList.Add(new MenuItem("Appointments", new List<MenuItem> { new MenuItem("Add Appointment", null, new MenuLink("Administrator", "Appointments", "Add"), null), new MenuItem("Manage Appointments", null, new MenuLink("Administrator", "Appointments", "Index"), null) }, new MenuLink("Administrator", "Appointments", "Index"), null, null, "fas fa-fw fa-calendar-check"));
            return administratorMainMenuList;
        }

        public static List<MenuItem> getManagerMainMenuList()
        {
            List<MenuItem> managerMainMenuList = new List<MenuItem>();
            managerMainMenuList.Add(new MenuItem("Dashboard", null, new MenuLink("Manager", "Dashboard", "Index"), null, null, "fas fa-fw fa-tachometer-alt"));
            managerMainMenuList.Add(new MenuItem("Buildings", new List<MenuItem> { new MenuItem("Add Building", null, new MenuLink("Manager", "Buildings", "Add"), null), new MenuItem("Manage Buildings", null, new MenuLink("Manager", "Buildings", "Index"), null) }, new MenuLink("Manager", "Buildings", "Index"), null, null, "fas fa-fw fa-city"));
            managerMainMenuList.Add(new MenuItem("Apartments", new List<MenuItem> { new MenuItem("Add Apartment", null, new MenuLink("Manager", "Apartments", "Add"), null), new MenuItem("Manage Apartments", null, new MenuLink("Manager", "Apartments", "Index"), null) }, new MenuLink("Manager", "Apartments", "Index"), null, null, "fas fa-fw fa-door-open"));
            managerMainMenuList.Add(new MenuItem("Appointments", new List<MenuItem> { new MenuItem("Add Appointment", null, new MenuLink("Manager", "Appointments", "Add"), null), new MenuItem("Manage Appointments", null, new MenuLink("Manager", "Appointments", "Index"), null) }, new MenuLink("Manager", "Appointments", "Index"), null, null, "fas fa-fw fa-calendar-check"));
            return managerMainMenuList;
        }
    }
}
