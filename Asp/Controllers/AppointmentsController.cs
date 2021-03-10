using Asp.Data;
using Asp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Controllers
{
    [Authorize]
    public class AppointmentsController : Controller
    {
        public readonly DatabaseContext context;
        public readonly UserManager<User> userManager;

        public AppointmentsController(DatabaseContext _context, UserManager<User> _userManager)
        {
            context = _context;
            userManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await context.Appointments.Where(x => x.TenantId.Contains(userManager.GetUserId(User)) || x.ManagerId.Contains(userManager.GetUserId(User))).ToListAsync();
            return View(appointments);
        }

        public async Task<IActionResult> Book(string id)
        {
            if (id == null || id == "")
            {
                return NotFound();
            }
            
            ViewBag.apartment = await context.Apartments.Include(x => x.Building).ThenInclude(X => X.Manager).Where(x => x.Id.Contains(id)).FirstOrDefaultAsync();
            ViewBag.TenantId = userManager.GetUserId(User);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Book(string id, [Bind("Id,Date,TenantId,ManagerId")] Appointment appointment)
        {
            ViewBag.TenantId = userManager.GetUserId(User);
            ViewBag.apartment = await context.Apartments.Include(x => x.Building).ThenInclude(X => X.Manager).Where(x => x.Id.Contains(id)).FirstOrDefaultAsync();
            if (ModelState.IsValid)
            {
                //ModelState.AddModelError(string.Empty, "YEAHHHHHH");
                //return View(appointment);
                var currentUserId = userManager.GetUserId(User);
                appointment.TenantId = currentUserId;
                appointment.Id = Guid.NewGuid().ToString();
                context.Add(appointment);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        private bool AppointmentExists(string id)
        {
            return context.Appointments.Any(e => e.Id == id);
        }
    }
}
