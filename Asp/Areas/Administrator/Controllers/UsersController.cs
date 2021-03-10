using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Asp.Data;
using Asp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Asp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {
        protected readonly RoleManager<Role> roleManager;
        protected readonly UserManager<User> userManager;
        protected readonly DatabaseContext _context;
        private readonly SignInManager<User> _signInManager;

        public UsersController(RoleManager<Role> roleManager, UserManager<User> userManager, DatabaseContext context, SignInManager<User> signInManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this._context = context;
            this._signInManager = signInManager;
        }
        // GET: UsersController
        public async Task<ActionResult> Index()
        {
            var currentUser = await userManager.FindByNameAsync(User.Identity.Name);
            var usersList = await userManager.Users.Where(x => x != currentUser).Include(u => u.UsersRoles).ThenInclude(ur => ur.Role).ToListAsync();
            return View(usersList);
        }

        public ActionResult Add()
        {
            ViewBag.Status = new SelectList(DefaultData.DefaultDataStatus, "Key", "Value");
            ViewBag.Role = _context.Roles.Where(r => !r.Name.Contains("Administrator")).Select(r => new SelectListItem { Text = r.Name, Value = r.Name });
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Firstname,Lastname,UserName,Email,Status,Role")] InputModel Input)
        {
            var user = new User { UserName = Input.UserName, Email = Input.Email, Firstname = Input.Firstname, Lastname = Input.Lastname};
            
            ViewBag.Status = new SelectList(DefaultData.DefaultDataStatus, "Key", "Value");
            ViewBag.Role = _context.Roles.Where(r => !r.Name.Contains("Administrator")).Select(r => new SelectListItem { Text = r.Name, Value = r.Name});
            if (ModelState.IsValid)
            {
                var result = await userManager.CreateAsync(user, "DefaultPassword0!");
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, Input.Role).Wait();
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }            
            return View(Input);
        }

        // GET: UsersController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: UsersController/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Status = new SelectList(DefaultData.DefaultDataStatus, "Key", "Value");
            ViewBag.Role = _context.Roles.Where(r => !r.Name.Contains("Administrator")).Select(r => new SelectListItem { Text = r.Name, Value = r.Name });
            var user = await _context.Users.Include(u => u.UsersRoles).ThenInclude(ur => ur.Role).Where(u => u.Id.Contains(id)).FirstOrDefaultAsync();
            if (user == null)
            {
                return NotFound();
            }
            return View(new InputModel { Id = user.Id, Firstname = user.Firstname, Lastname = user.Lastname, Email = user.Email, UserName = user.UserName, Status = user.Status, Role = user.UsersRoles.First().Role.Name});
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string id, [Bind("Id,Firstname,Lastname,UserName,Email,Status,Role")] InputModel Input)
        {
            
            if (id != Input.Id)
            {
                return NotFound();
            }
            ViewBag.Status = new SelectList(DefaultData.DefaultDataStatus, "Key", "Value");
            ViewBag.Role = _context.Roles.Where(r => !r.Name.Contains("Administrator")).Select(r => new SelectListItem { Text = r.Name, Value = r.Name });
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(Input.Id);
                if (user == null)
                {
                    return NotFound();
                }
                user.Firstname = Input.Firstname;
                user.Lastname = Input.Lastname;
                user.UserName = Input.UserName;
                user.Email = Input.Email;
                user.Status = Input.Status;

                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    var roleResult = await UpdateRoles(user, Input.Role);
                    if (roleResult.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var error in roleResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    //return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(Input);
            
        }
        [AllowAnonymous]
        public async Task<IdentityResult> UpdateRoles(User user, string roleName)
        {
            var currentUser = await userManager.GetUserAsync(this.User);
            var allRoles = await userManager.GetRolesAsync(user);
            await _signInManager.SignOutAsync();

            await _signInManager.SignInAsync(user, isPersistent: false);
            var result = await userManager.RemoveFromRolesAsync(user, allRoles.ToArray());
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(currentUser, isPersistent: false);
            //await userManager.AddToRoleAsync(user, roleName);
            return result;
        }

        // GET: UsersController/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UsersController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Restore(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Administrator/Buildings/Delete/5
        [HttpPost, ActionName("Restore")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestoreConfirmed(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            user.Status = "A";
            await userManager.UpdateAsync(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.AnyAsync(e => e.Id == id).Result;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


    public class InputModel
    {
        public string Id { set;get; }

        [Required]
        [Display(Name = "First Name")]
        public string Firstname { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string Lastname { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Account Status")]
        public string Status { set; get; } = "A";

        [Required]
        [Display(Name = "Account Type")]
        public string Role { set; get; }
    }
}
