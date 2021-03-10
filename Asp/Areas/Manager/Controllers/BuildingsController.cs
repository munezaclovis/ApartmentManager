using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asp.Data;
using Asp.Models;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;

namespace Asp.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Administrator, Manager")]
    public class BuildingsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public BuildingsController(DatabaseContext context, UserManager<User> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        // GET: Administrator/Buildings
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            return View(_context.Buildings.Where(x => x.ManagerId.Contains(userId)).ToList());
        }

        // GET: Administrator/Buildings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var building = await _context.Buildings.Include(b => b.Manager)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (building == null)
            {
                return NotFound();
            }

            return View(building);
        }

        // GET: Administrator/Buildings/Create
        public IActionResult Add()
        {
            ViewBag.Status = new SelectList(DefaultData.DefaultDataStatus, "Key", "Value");
            ViewBag.ManagerId = _context.Users.Where(x => x.UsersRoles.Select(y=>y.Role.Name.Contains("Manager")).First()).Select(u => new SelectListItem { Text = u.UserName + " - " + u.Email, Value = u.Id });
            return View();
        }

        // POST: Administrator/Buildings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Address,City,State,Country,PostalCode, Status, ManagerId")] Building building)
        {
            ViewBag.Status = new SelectList(DefaultData.DefaultDataStatus, "Key", "Value");
            ViewBag.ManagerId = _context.Users.Where(x => x.UsersRoles.Select(y => y.Role.Name.Contains("Manager")).First()).Select(u => new SelectListItem { Text = u.UserName + " - " + u.Email, Value = u.Id });
            if (ModelState.IsValid)
            {
                building.ManagerId = _userManager.GetUserId(User);
                _context.Add(building);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(building);
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
            var building = await _context.Buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }
            return View(building);
        }

        // POST: Administrator/Buildings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Address,City,State,Country,PostalCode, Status, ManagerId")] Building building)
        {
            if (id != building.Id)
            {
                return NotFound();
            }
            ViewBag.Status = new SelectList(DefaultData.DefaultDataStatus, "Key", "Value");
            ViewBag.ManagerId = _context.Users.Where(x => x.UsersRoles.Select(y => y.Role.Name.Contains("Manager")).First()).Select(u => new SelectListItem { Text = u.UserName + " - " + u.Email, Value = u.Id });
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(building);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingExists(building.Id))
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
            return View(building);
        }

        // GET: Administrator/Buildings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var building = await _context.Buildings.Include(b => b.Manager).FirstOrDefaultAsync(m => m.Id == id);
            if (building == null)
            {
                return NotFound();
            }

            return View(building);
        }

        // POST: Administrator/Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var building = await _context.Buildings.FindAsync(id);
            _context.Buildings.Remove(building);
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

            var building = await _context.Buildings.Include(b => b.Manager).FirstOrDefaultAsync(m => m.Id == id);
            if (building == null)
            {
                return NotFound();
            }

            return View(building);
        }

        // POST: Administrator/Buildings/Delete/5
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmed(string id)
        {
            var building = await _context.Buildings.FindAsync(id);
            building.Status = "A";
            _context.Buildings.Update(building);
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
