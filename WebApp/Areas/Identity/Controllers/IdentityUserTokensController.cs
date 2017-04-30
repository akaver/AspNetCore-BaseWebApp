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
    public class IdentityUserTokensController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IdentityUserTokensController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Identity/IdentityUserTokens
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IdentityUserTokens.Include(i => i.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Identity/IdentityUserTokens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserToken = await _context.IdentityUserTokens
                .Include(i => i.User)
                .SingleOrDefaultAsync(m => m.IdentityUserTokenId == id);
            if (identityUserToken == null)
            {
                return NotFound();
            }

            return View(identityUserToken);
        }

        // GET: Identity/IdentityUserTokens/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator");
            return View();
        }

        // POST: Identity/IdentityUserTokens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdentityUserTokenId,UserId,LoginProvider,Name,Value")] IdentityUserToken identityUserToken)
        {
            if (ModelState.IsValid)
            {
                _context.Add(identityUserToken);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator", identityUserToken.UserId);
            return View(identityUserToken);
        }

        // GET: Identity/IdentityUserTokens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserToken = await _context.IdentityUserTokens.SingleOrDefaultAsync(m => m.IdentityUserTokenId == id);
            if (identityUserToken == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator", identityUserToken.UserId);
            return View(identityUserToken);
        }

        // POST: Identity/IdentityUserTokens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdentityUserTokenId,UserId,LoginProvider,Name,Value")] IdentityUserToken identityUserToken)
        {
            if (id != identityUserToken.IdentityUserTokenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(identityUserToken);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdentityUserTokenExists(identityUserToken.IdentityUserTokenId))
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
            ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator", identityUserToken.UserId);
            return View(identityUserToken);
        }

        // GET: Identity/IdentityUserTokens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserToken = await _context.IdentityUserTokens
                .Include(i => i.User)
                .SingleOrDefaultAsync(m => m.IdentityUserTokenId == id);
            if (identityUserToken == null)
            {
                return NotFound();
            }

            return View(identityUserToken);
        }

        // POST: Identity/IdentityUserTokens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var identityUserToken = await _context.IdentityUserTokens.SingleOrDefaultAsync(m => m.IdentityUserTokenId == id);
            _context.IdentityUserTokens.Remove(identityUserToken);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool IdentityUserTokenExists(int id)
        {
            return _context.IdentityUserTokens.Any(e => e.IdentityUserTokenId == id);
        }
    }
}
