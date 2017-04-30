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
    public class IdentityUserClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IdentityUserClaimsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Identity/IdentityUserClaims
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IdentityUserClaims.Include(i => i.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Identity/IdentityUserClaims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserClaim = await _context.IdentityUserClaims
                .Include(i => i.User)
                .SingleOrDefaultAsync(m => m.IdentityUserClaimId == id);
            if (identityUserClaim == null)
            {
                return NotFound();
            }

            return View(identityUserClaim);
        }

        // GET: Identity/IdentityUserClaims/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator");
            return View();
        }

        // POST: Identity/IdentityUserClaims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdentityUserClaimId,UserId,ClaimType,ClaimValue")] IdentityUserClaim identityUserClaim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(identityUserClaim);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator", identityUserClaim.UserId);
            return View(identityUserClaim);
        }

        // GET: Identity/IdentityUserClaims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserClaim = await _context.IdentityUserClaims.SingleOrDefaultAsync(m => m.IdentityUserClaimId == id);
            if (identityUserClaim == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator", identityUserClaim.UserId);
            return View(identityUserClaim);
        }

        // POST: Identity/IdentityUserClaims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdentityUserClaimId,UserId,ClaimType,ClaimValue")] IdentityUserClaim identityUserClaim)
        {
            if (id != identityUserClaim.IdentityUserClaimId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(identityUserClaim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdentityUserClaimExists(identityUserClaim.IdentityUserClaimId))
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
            ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator", identityUserClaim.UserId);
            return View(identityUserClaim);
        }

        // GET: Identity/IdentityUserClaims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserClaim = await _context.IdentityUserClaims
                .Include(i => i.User)
                .SingleOrDefaultAsync(m => m.IdentityUserClaimId == id);
            if (identityUserClaim == null)
            {
                return NotFound();
            }

            return View(identityUserClaim);
        }

        // POST: Identity/IdentityUserClaims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var identityUserClaim = await _context.IdentityUserClaims.SingleOrDefaultAsync(m => m.IdentityUserClaimId == id);
            _context.IdentityUserClaims.Remove(identityUserClaim);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool IdentityUserClaimExists(int id)
        {
            return _context.IdentityUserClaims.Any(e => e.IdentityUserClaimId == id);
        }
    }
}
