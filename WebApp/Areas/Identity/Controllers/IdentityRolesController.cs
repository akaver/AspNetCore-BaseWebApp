using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Identity.Uow.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspNetCore.Identity.Uow.Models;
using DAL;
using DAL.EntityFrameworkCore;

namespace WebApp.Areas.Identity.Controllers
{
    [Area("Identity")]
    public class IdentityRolesController : Controller
    {
        private readonly IIdentityUnitOfWork _uow;

        public IdentityRolesController(IIdentityUnitOfWork uow)
        {
            _uow = uow;    
        }

        // GET: Identity/IdentityRoles
        public async Task<IActionResult> Index()
        {
            return View(model: await _uow.IdentityRoles.AllAsync());
        }

        // GET: Identity/IdentityRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityRole = await _uow.IdentityRoles.FindAsync(id);
            if (identityRole == null)
            {
                return NotFound();
            }

            return View(identityRole);
        }

        // GET: Identity/IdentityRoles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Identity/IdentityRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole identityRole)
        {
            if (ModelState.IsValid)
            {
                _uow.IdentityRoles.Add(identityRole);
                await _uow.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(identityRole);
        }

        // GET: Identity/IdentityRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityRole = await _uow.IdentityRoles.FindAsync(id);
            if (identityRole == null)
            {
                return NotFound();
            }
            return View(identityRole);
        }

        // POST: Identity/IdentityRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IdentityRole identityRole)
        {
            if (id != identityRole.IdentityRoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.IdentityRoles.Update(identityRole);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdentityRoleExists(identityRole.IdentityRoleId))
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
            return View(identityRole);
        }

        // GET: Identity/IdentityRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityRole = await _uow.IdentityRoles.FindAsync(id);
            if (identityRole == null)
            {
                return NotFound();
            }

            return View(identityRole);
        }

        // POST: Identity/IdentityRoles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _uow.IdentityRoles.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool IdentityRoleExists(int id)
        {
            return _uow.IdentityRoles.Exists(id);
        }
    }
}
