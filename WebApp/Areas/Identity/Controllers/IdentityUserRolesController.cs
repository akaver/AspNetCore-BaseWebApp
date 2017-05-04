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
using Domain;
using Microsoft.AspNetCore.Authorization;
using WebApp.Areas.Identity.ViewModels;

namespace WebApp.Areas.Identity.Controllers
{
    [Area(areaName: "Identity")]
    [Authorize(Roles = "Admin")]
    public class IdentityUserRolesController : Controller
    {
        private readonly IIdentityUnitOfWork<ApplicationUser> _uow;

        public IdentityUserRolesController(IIdentityUnitOfWork<ApplicationUser> uow)
        {
            _uow = uow;    
        }

        // GET: Identity/IdentityUserRoles
        public async Task<IActionResult> Index()
        {
            var userRoles = await _uow.IdentityUserRoles.AllIncludeRoleAndUserAsync();
            return View(model: userRoles);
        }

        // GET: Identity/IdentityUserRoles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserRole = await _uow.IdentityUserRoles.SingleIncludeUserAndRoleAsync(id: id.Value);
            if (identityUserRole == null)
            {
                return NotFound();
            }

            return View(model: identityUserRole);
        }

        // GET: Identity/IdentityUserRoles/Create
        public IActionResult Create()
        {
            var vm = new IdentityUserRolesCreateEditViewModel()
            {
                UserSelectList = new SelectList(items: _uow.IdentityUsers.All(), dataValueField: nameof(IdentityUser.IdentityUserId), dataTextField: nameof(IdentityUser.Email)),
                RoleSelectList = new SelectList(items: _uow.IdentityRoles.All(), dataValueField: nameof(IdentityRole.IdentityRoleId), dataTextField: nameof(IdentityRole.Name)),
            };
            return View(model: vm);
        }

        // POST: Identity/IdentityUserRoles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityUserRolesCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.IdentityUserRoles.Add(entity: vm.IdentityUserRole);
                await _uow.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            vm.UserSelectList = new SelectList(items: _uow.IdentityUsers.All(), dataValueField: nameof(IdentityUser.IdentityUserId),
                dataTextField: nameof(IdentityUser.Email), selectedValue: vm.IdentityUserRole.UserId);
            vm.RoleSelectList = new SelectList(items: _uow.IdentityRoles.All(), dataValueField: nameof(IdentityRole.IdentityRoleId),
                dataTextField: nameof(IdentityRole.Name), selectedValue: vm.IdentityUserRole.RoleId);

            return View(model: vm);
        }

        // GET: Identity/IdentityUserRoles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserRole = await _uow.IdentityUserRoles.FindAsync(id);
            if (identityUserRole == null)
            {
                return NotFound();
            }

            var vm = new IdentityUserRolesCreateEditViewModel()
            {
                IdentityUserRole = identityUserRole,
                UserSelectList = new SelectList(items: _uow.IdentityUsers.All(), dataValueField: nameof(IdentityUser.IdentityUserId), dataTextField: nameof(IdentityUser.Email), selectedValue: identityUserRole.UserId),
                RoleSelectList = new SelectList(items: _uow.IdentityRoles.All(), dataValueField: nameof(IdentityRole.IdentityRoleId), dataTextField: nameof(IdentityRole.Name), selectedValue: identityUserRole.RoleId),

            };
            return View(model: vm);
        }

        // POST: Identity/IdentityUserRoles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IdentityUserRolesCreateEditViewModel vm)
        {
            if (id != vm.IdentityUserRole.IdentityUserRoleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.IdentityUserRoles.Update(entity: vm.IdentityUserRole);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_uow.IdentityUserRoles.Exists(id: vm.IdentityUserRole.IdentityUserRoleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(actionName: nameof(Index));
            }
            vm.UserSelectList = new SelectList(items: _uow.IdentityUsers.All(),
                dataValueField: nameof(IdentityUser.IdentityUserId), dataTextField: nameof(IdentityUser.Email),
                selectedValue: vm.IdentityUserRole.UserId);
            vm.RoleSelectList = new SelectList(items: _uow.IdentityRoles.All(),
                dataValueField: nameof(IdentityRole.IdentityRoleId), dataTextField: nameof(IdentityRole.Name),
                selectedValue: vm.IdentityUserRole.RoleId);

            return View(model: vm);
        }

        // GET: Identity/IdentityUserRoles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserRole = await _uow.IdentityUserRoles.SingleIncludeUserAndRoleAsync(id: id.Value);
            if (identityUserRole == null)
            {
                return NotFound();
            }

            return View(model: identityUserRole);
        }

        // POST: Identity/IdentityUserRoles/Delete/5
        [HttpPost, ActionName(name: nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _uow.IdentityUserRoles.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(actionName: nameof(Index));
        }

    }
}
