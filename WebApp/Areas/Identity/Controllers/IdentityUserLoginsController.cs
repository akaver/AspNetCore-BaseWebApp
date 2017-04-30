using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCore.Identity.Uow.Models;
using DAL.EntityFrameworkCore;

namespace WebApp.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class IdentityUserLoginsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IdentityUserLoginsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Identity/IdentityUserLogins
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IdentityUserLogins.Include(i => i.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Identity/IdentityUserLogins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserLogin = await _context.IdentityUserLogins
                .Include(i => i.User)
                .SingleOrDefaultAsync(m => m.IdentityUserLoginId == id);
            if (identityUserLogin == null)
            {
                return NotFound();
            }

            return View(identityUserLogin);
        }

        // GET: Identity/IdentityUserLogins/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator");
            return View();
        }

        // POST: Identity/IdentityUserLogins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdentityUserLoginId,LoginProvider,ProviderKey,ProviderDisplayName,UserId")] IdentityUserLogin identityUserLogin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(identityUserLogin);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator", identityUserLogin.UserId);
            return View(identityUserLogin);
        }

        // GET: Identity/IdentityUserLogins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserLogin = await _context.IdentityUserLogins.SingleOrDefaultAsync(m => m.IdentityUserLoginId == id);
            if (identityUserLogin == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator", identityUserLogin.UserId);
            return View(identityUserLogin);
        }

        // POST: Identity/IdentityUserLogins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdentityUserLoginId,LoginProvider,ProviderKey,ProviderDisplayName,UserId")] IdentityUserLogin identityUserLogin)
        {
            if (id != identityUserLogin.IdentityUserLoginId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(identityUserLogin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdentityUserLoginExists(identityUserLogin.IdentityUserLoginId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator", identityUserLogin.UserId);
            return View(identityUserLogin);
        }

        // GET: Identity/IdentityUserLogins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserLogin = await _context.IdentityUserLogins
                .Include(i => i.User)
                .SingleOrDefaultAsync(m => m.IdentityUserLoginId == id);
            if (identityUserLogin == null)
            {
                return NotFound();
            }

            return View(identityUserLogin);
        }

        // POST: Identity/IdentityUserLogins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var identityUserLogin = await _context.IdentityUserLogins.SingleOrDefaultAsync(m => m.IdentityUserLoginId == id);
            _context.IdentityUserLogins.Remove(identityUserLogin);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool IdentityUserLoginExists(int id)
        {
            return _context.IdentityUserLogins.Any(e => e.IdentityUserLoginId == id);
        }
    }
}
