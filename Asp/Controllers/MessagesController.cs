using Asp.Data;
using Asp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Asp.Controllers
{
    public class MessagesController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly UserManager<User> _userManager;

        public MessagesController(DatabaseContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Messages
        public async Task<IActionResult> Index()
        {
            ViewBag.SentMessages = await _context.Messages.Where(x => x.Sender.Contains(_userManager.GetUserId(User))).ToListAsync();
            ViewBag.ReceivedMessages = await _context.Messages.Where(x => x.Receiver.Contains(_userManager.GetUserId(User))).ToListAsync();
            return View();
        }

        
        // GET: Messages/Create
        public IActionResult Create()
        {
            ViewBag.currentUser = _userManager.GetUserId(User);
            ViewBag.Users = new SelectList(_context.Users.Where(x => !x.Id.Contains(_userManager.GetUserId(User))), "Id", "Email");
            return View(new Message { Id = Guid.NewGuid().ToString()});
        }

        // POST: Messages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Text,Date,Sender,Receiver")] Message message)
        {
            if (ModelState.IsValid)
            {
                _context.Add(message);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.currentUser = _userManager.GetUserId(User);
            ViewBag.Users = new SelectList(_context.Users.Where(x => !x.Id.Contains(_userManager.GetUserId(User))), "Id", "Email", message.Receiver);
            return View(message);
        }

        private bool MessageExists(string id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}