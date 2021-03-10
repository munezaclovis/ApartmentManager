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

namespace Asp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Administrator")]
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
            var appointments = await _context.Appointments.ToListAsync();
            return View(appointments);
        }
        // GET: Administrator/Buildings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Administrator/Buildings/Create
        public IActionResult Add()
        {
            ViewBag.TenantId = _context.Users.Where(x => x.UsersRoles.Select(y => y.Role.Name.Contains("Tenant")).First()).Select(u => new SelectListItem { Text = u.UserName + " - " + u.Email, Value = u.Id });
            ViewBag.ManagerId = _context.Users.Where(x => x.UsersRoles.Select(y => y.Role.Name.Contains("Manager")).First()).Select(u => new SelectListItem { Text = u.UserName + " - " + u.Email, Value = u.Id });
            return View();
        }

        // POST: Administrator/Buildings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Date,ManagerId,TenantId")]Appointment appointment)
        {
            ViewBag.TenantId = _context.Users.Where(x => x.UsersRoles.Select(y => y.Role.Name.Contains("Tenant")).First()).Select(u => new SelectListItem { Text = u.UserName + " - " + u.Email, Value = u.Id });
            ViewBag.ManagerId = _context.Users.Where(x => x.UsersRoles.Select(y => y.Role.Name.Contains("Manager")).First()).Select(u => new SelectListItem { Text = u.UserName + " - " + u.Email, Value = u.Id });
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
            ViewBag.Status = new SelectList(DefaultData.DefaultDataStatus, "Key", "Value");
            ViewBag.ManagerId = _context.Users.Where(x => x.UsersRoles.Select(y => y.Role.Name.Contains("Manager")).First()).Select(u => new SelectListItem { Text = u.UserName + " - " + u.Email, Value = u.Id });
            var appointment = await _context.Buildings.FindAsync(id);
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
            ViewBag.Status = new SelectList(DefaultData.DefaultDataStatus, "Key", "Value");
            ViewBag.ManagerId = _context.Users.Where(x => x.UsersRoles.Select(y => y.Role.Name.Contains("Manager")).First()).Select(u => new SelectListItem { Text = u.UserName + " - " + u.Email, Value = u.Id });
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

            var appointment = await _context.Buildings.Include(b => b.Manager).FirstOrDefaultAsync(m => m.Id == id);
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
            var appointment = await _context.Buildings.FindAsync(id);
            _context.Buildings.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Administrator/Buildings/Delete/5
        public async Task<IActionResult> Restore(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Buildings.Include(b => b.Manager).FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Administrator/Buildings/Delete/5
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmed(string id)
        {
            var appointment = await _context.Buildings.FindAsync(id);
            appointment.Status = "A";
            _context.Buildings.Update(appointment);
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
