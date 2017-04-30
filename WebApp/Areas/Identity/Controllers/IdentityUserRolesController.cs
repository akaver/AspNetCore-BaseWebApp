using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCore.Identity.Uow.Models;
using DAL.EntityFrameworkCore;

namespace WebApp.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class IdentityUserRolesController : Controller
    {
        private readonly IIdentityUnitOfWork _uow;

        public IdentityUserRolesController(IIdentityUnitOfWork uow)
        {
            _uow = uow;    
        }

        // GET: Identity/IdentityUserRoles
        public async Task<IActionResult> Index()
        {
            var userRoles = await _uow.IdentityUserRoles.AllAsync(); //.Include(i => i.Role).Include(i => i.User);
            return View(userRoles);
        }

        //// GET: Identity/IdentityUserRoles/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var identityUserRole = await _uow.IdentityUserRoles.FindAsync(id);
        //    if (identityUserRole == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(identityUserRole);
        //}

        //// GET: Identity/IdentityUserRoles/Create
        //public IActionResult Create()
        //{
        //    ViewData["RoleId"] = new SelectList(_context.IdentityRoles, "IdentityRoleId", "Discriminator");
        //    ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator");
        //    return View();
        //}

        //// POST: Identity/IdentityUserRoles/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("IdentityUserRoleId,UserId,RoleId")] IdentityUserRole identityUserRole)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(identityUserRole);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["RoleId"] = new SelectList(_context.IdentityRoles, "IdentityRoleId", "Discriminator", identityUserRole.RoleId);
        //    ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator", identityUserRole.UserId);
        //    return View(identityUserRole);
        //}

        //// GET: Identity/IdentityUserRoles/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var identityUserRole = await _context.IdentityUserRoles.SingleOrDefaultAsync(m => m.IdentityUserRoleId == id);
        //    if (identityUserRole == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["RoleId"] = new SelectList(_context.IdentityRoles, "IdentityRoleId", "Discriminator", identityUserRole.RoleId);
        //    ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator", identityUserRole.UserId);
        //    return View(identityUserRole);
        //}

        //// POST: Identity/IdentityUserRoles/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("IdentityUserRoleId,UserId,RoleId")] IdentityUserRole identityUserRole)
        //{
        //    if (id != identityUserRole.IdentityUserRoleId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(identityUserRole);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!IdentityUserRoleExists(identityUserRole.IdentityUserRoleId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["RoleId"] = new SelectList(_context.IdentityRoles, "IdentityRoleId", "Discriminator", identityUserRole.RoleId);
        //    ViewData["UserId"] = new SelectList(_context.IdentityUsers, "IdentityUserId", "Discriminator", identityUserRole.UserId);
        //    return View(identityUserRole);
        //}

        //// GET: Identity/IdentityUserRoles/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var identityUserRole = await _context.IdentityUserRoles
        //        .Include(i => i.Role)
        //        .Include(i => i.User)
        //        .SingleOrDefaultAsync(m => m.IdentityUserRoleId == id);
        //    if (identityUserRole == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(identityUserRole);
        //}

        //// POST: Identity/IdentityUserRoles/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var identityUserRole = await _context.IdentityUserRoles.SingleOrDefaultAsync(m => m.IdentityUserRoleId == id);
        //    _context.IdentityUserRoles.Remove(identityUserRole);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //private bool IdentityUserRoleExists(int id)
        //{
        //    return _context.IdentityUserRoles.Any(e => e.IdentityUserRoleId == id);
        //}
    }
}
