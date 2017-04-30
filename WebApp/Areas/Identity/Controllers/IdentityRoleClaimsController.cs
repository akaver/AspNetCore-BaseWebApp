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
    public class IdentityRoleClaimsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IdentityRoleClaimsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Identity/IdentityRoleClaims
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.IdentityRoleClaims.Include(i => i.Role);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Identity/IdentityRoleClaims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityRoleClaim = await _context.IdentityRoleClaims
                .Include(i => i.Role)
                .SingleOrDefaultAsync(m => m.IdentityRoleClaimId == id);
            if (identityRoleClaim == null)
            {
                return NotFound();
            }

            return View(identityRoleClaim);
        }

        // GET: Identity/IdentityRoleClaims/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.IdentityRoles, "IdentityRoleId", "Discriminator");
            return View();
        }

        // POST: Identity/IdentityRoleClaims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdentityRoleClaimId,RoleId,ClaimType,ClaimValue")] IdentityRoleClaim identityRoleClaim)
        {
            if (ModelState.IsValid)
            {
                _context.Add(identityRoleClaim);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["RoleId"] = new SelectList(_context.IdentityRoles, "IdentityRoleId", "Discriminator", identityRoleClaim.RoleId);
            return View(identityRoleClaim);
        }

        // GET: Identity/IdentityRoleClaims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityRoleClaim = await _context.IdentityRoleClaims.SingleOrDefaultAsync(m => m.IdentityRoleClaimId == id);
            if (identityRoleClaim == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.IdentityRoles, "IdentityRoleId", "Discriminator", identityRoleClaim.RoleId);
            return View(identityRoleClaim);
        }

        // POST: Identity/IdentityRoleClaims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdentityRoleClaimId,RoleId,ClaimType,ClaimValue")] IdentityRoleClaim identityRoleClaim)
        {
            if (id != identityRoleClaim.IdentityRoleClaimId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(identityRoleClaim);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdentityRoleClaimExists(identityRoleClaim.IdentityRoleClaimId))
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
            ViewData["RoleId"] = new SelectList(_context.IdentityRoles, "IdentityRoleId", "Discriminator", identityRoleClaim.RoleId);
            return View(identityRoleClaim);
        }

        // GET: Identity/IdentityRoleClaims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityRoleClaim = await _context.IdentityRoleClaims
                .Include(i => i.Role)
                .SingleOrDefaultAsync(m => m.IdentityRoleClaimId == id);
            if (identityRoleClaim == null)
            {
                return NotFound();
            }

            return View(identityRoleClaim);
        }

        // POST: Identity/IdentityRoleClaims/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var identityRoleClaim = await _context.IdentityRoleClaims.SingleOrDefaultAsync(m => m.IdentityRoleClaimId == id);
            _context.IdentityRoleClaims.Remove(identityRoleClaim);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool IdentityRoleClaimExists(int id)
        {
            return _context.IdentityRoleClaims.Any(e => e.IdentityRoleClaimId == id);
        }
    }
}
