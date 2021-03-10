using Asp.Data;
using Asp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Areas.Manager.Controllers
{
    [Area("Manager")]
    [Authorize(Roles = "Administrator, Manager")]
    public class ApartmentsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public ApartmentsController(DatabaseContext context, UserManager<User> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

        // GET: Administrator/Buildings
        public async Task<IActionResult> Index()
        {
            return View(await _context.Apartments.Include(A => A.Building).Where(x => x.Building.ManagerId == _userManager.GetUserId(User)).ToListAsync());
        }

        // GET: Administrator/Buildings/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var building = await _context.Apartments.Include(b => b.Building).FirstOrDefaultAsync(m => m.Id == id);
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
            ViewBag.BuildingId = _context.Buildings.Where(x => x.ManagerId.Contains(_userManager.GetUserId(User))).Select(A => new SelectListItem { Text = A.getFullAddress(), Value = A.Id });
            return View();
        }

        // POST: Administrator/Buildings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Number,Floor,Bedrooms,Bathrooms,BuildingId,Status")] Apartment apartment)
        {
            ViewBag.Status = new SelectList(DefaultData.DefaultDataStatus, "Key", "Value");
            ViewBag.BuildingId = _context.Buildings.Select(A => new SelectListItem { Text = A.getFullAddress(), Value = A.Id });
            if (ModelState.IsValid)
            {
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(apartment);
        }

        // GET: Administrator/Buildings/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Status = new SelectList(DefaultData.DefaultDataStatus, "Key", "Value");
            ViewBag.BuildingId = _context.Buildings.Select(A => new SelectListItem { Text = A.getFullAddress(), Value = A.Id });
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }
            return View(apartment);
        }

        // POST: Administrator/Buildings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Number, Floor, Bedrooms, Bathrooms, BuildingId, Status")] Apartment apartment)
        {
            if (id != apartment.Id)
            {
                return NotFound();
            }
            ViewBag.Status = new SelectList(DefaultData.DefaultDataStatus, "Key", "Value");
            ViewBag.BuildingId = _context.Apartments.Include(A => A.Building).ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.Id))
                    {
                        return NotFound("Apartment not Found");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(apartment);
        }

        // GET: Administrator/Buildings/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments.Include(A => A.Building).FirstOrDefaultAsync(m => m.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Administrator/Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            _context.Apartments.Remove(apartment);
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

            var apartment = await _context.Apartments.Include(b => b.Building).FirstOrDefaultAsync(m => m.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // POST: Administrator/Buildings/Delete/5
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmed(string id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            apartment.Status = "A";
            _context.Apartments.Update(apartment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentExists(string id)
        {
            return _context.Apartments.Any(e => e.Id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
