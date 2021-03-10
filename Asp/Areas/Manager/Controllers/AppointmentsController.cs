using Asp.Data;
using Asp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Administrator, Manager")]
    public class AppointmentsController : Controller
    {
        public readonly DatabaseContext _context;
        public readonly UserManager<User> userManager;

        public AppointmentsController(DatabaseContext context, UserManager<User> _userManager)
        {
            _context = context;
            userManager = _userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await userManager.GetUserAsync(User);
            var appointments = await _context.Appointments.Where(x => x.ManagerId.Contains(currentUser.Id)).ToListAsync();
            return View(appointments);
        }
        // GET: Administrator/Buildings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var currentUser = await userManager.GetUserAsync(User);
            var appointment = await _context.Appointments.Where(x => x.ManagerId.Contains(currentUser.Id))
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Administrator/Buildings/Create
        public async Task<IActionResult> Add()
        {
            var currentUser = await userManager.GetUserAsync(User);
            ViewBag.TenantId = _context.Users.Where(x => x.UsersRoles.Select(y => y.Role.Name.Contains("Tenant")).First()).Select(u => new SelectListItem { Text = u.UserName + " - " + u.Email, Value = u.Id });
            ViewBag.ManagerId = new SelectListItem { Text = currentUser.UserName + " - " + currentUser.Email, Value = currentUser.Id };
            return View();
        }

        // POST: Administrator/Buildings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Date,ManagerId,TenantId")]Appointment appointment)
        {
            var currentUser = await userManager.GetUserAsync(User);
            ViewBag.TenantId = _context.Users.Where(x => x.UsersRoles.Select(y => y.Role.Name.Contains("Tenant")).First()).Select(u => new SelectListItem { Text = u.UserName + " - " + u.Email, Value = u.Id });
            ViewBag.ManagerId = new SelectListItem { Text = currentUser.UserName + " - " + currentUser.Email, Value = currentUser.Id }; 
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Administrator/Buildings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var currentUser = await userManager.GetUserAsync(User);
            ViewBag.TenantId = _context.Users.Where(x => x.UsersRoles.Select(y => y.Role.Name.Contains("Tenant")).First()).Select(u => new SelectListItem { Text = u.UserName + " - " + u.Email, Value = u.Id });
            ViewBag.ManagerId = new SelectListItem { Text = currentUser.UserName + " - " + currentUser.Email, Value = currentUser.Id }; 
            var appointment = _context.Appointments.Where(x => x.ManagerId.Contains(currentUser.Id)).FirstOrDefault();
            if (appointment == null)
            {
                return NotFound();
            }
            return View(appointment);
        }

        // POST: Administrator/Buildings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Date,ManagerId,TenantId")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }
            var currentUser = await userManager.GetUserAsync(User);
            ViewBag.TenantId = _context.Users.Where(x => x.UsersRoles.Select(y => y.Role.Name.Contains("Tenant")).First()).Select(u => new SelectListItem { Text = u.UserName + " - " + u.Email, Value = u.Id });
            ViewBag.ManagerId = new SelectListItem { Text = currentUser.UserName + " - " + currentUser.Email, Value = currentUser.Id }; 
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingExists(appointment.Id))
                    {
                        return NotFound("Building not Found");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Administrator/Buildings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var currentUser = await userManager.GetUserAsync(User);
            var appointment = _context.Appointments.Where(x => x.ManagerId.Contains(currentUser.Id)).FirstOrDefault(e => e.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Administrator/Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuildingExists(string id)
        {
            return _context.Buildings.Any(e => e.Id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
