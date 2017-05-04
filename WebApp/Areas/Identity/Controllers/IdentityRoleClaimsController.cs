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
    public class IdentityRoleClaimsController : Controller
    {
        private readonly IIdentityUnitOfWork<ApplicationUser> _uow;

        public IdentityRoleClaimsController(IIdentityUnitOfWork<ApplicationUser> uow)
        {
            _uow = uow;    
        }

        // GET: Identity/IdentityRoleClaims
        public async Task<IActionResult> Index()
        {
            var roleClaims = await _uow.IdentityRoleClaims.AllIncludeRoleAsync();
            return View(model: roleClaims);
        }

        // GET: Identity/IdentityRoleClaims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roleClaim = await _uow.IdentityRoleClaims.SingleByIdIncludeRole(id: id.Value);

            if (roleClaim == null)
            {
                return NotFound();
            }

            return View(model: roleClaim);
        }

        // GET: Identity/IdentityRoleClaims/Create
        public IActionResult Create()
        {
            var vm = new IdentityRoleClaimsCreateEditViewModel()
            {
                RoleSelectList = new SelectList(items: _uow.IdentityRoles.All(), dataValueField: nameof(IdentityRole.IdentityRoleId),dataTextField: nameof(IdentityRole.Name))
        };
            return View(model: vm);
        }

        // POST: Identity/IdentityRoleClaims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRoleClaimsCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.IdentityRoleClaims.Add(entity: vm.IdentityRoleClaim);
                await _uow.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            vm.RoleSelectList = new SelectList(items: _uow.IdentityRoles.All(), dataValueField: nameof(IdentityRole.IdentityRoleId),
                dataTextField: nameof(IdentityRole.Name), selectedValue: vm.IdentityRoleClaim.IdentityRoleClaimId);
            return View(model: vm);
        }

        // GET: Identity/IdentityRoleClaims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityRoleClaim = await _uow.IdentityRoleClaims.SingleByIdIncludeRole(id: id.Value);
            if (identityRoleClaim == null)
            {
                return NotFound();
            }

            var vm = new IdentityRoleClaimsCreateEditViewModel()
            {
                RoleSelectList = new SelectList(items: _uow.IdentityRoles.All(), dataValueField: nameof(IdentityRole.IdentityRoleId), dataTextField: nameof(IdentityRole.Name), selectedValue: identityRoleClaim.RoleId)
            };

            return View(model: vm);
        }

        // POST: Identity/IdentityRoleClaims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IdentityRoleClaimsCreateEditViewModel vm)
        {
            if (id != vm.IdentityRoleClaim.IdentityRoleClaimId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.IdentityRoleClaims.Update(entity: vm.IdentityRoleClaim);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_uow.IdentityRoleClaims.Exists(id: vm.IdentityRoleClaim.IdentityRoleClaimId))
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
            vm.RoleSelectList = new SelectList(items: _uow.IdentityRoles.All(), dataValueField: nameof(IdentityRole.IdentityRoleId),
                dataTextField: nameof(IdentityRole.Name), selectedValue: vm.IdentityRoleClaim.RoleId);
            return View(model: vm);
        }

        // GET: Identity/IdentityRoleClaims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityRoleClaim = await _uow.IdentityRoleClaims.SingleByIdIncludeRole(id: id.Value);
            if (identityRoleClaim == null)
            {
                return NotFound();
            }

            return View(model: identityRoleClaim);
        }

        // POST: Identity/IdentityRoleClaims/Delete/5
        [HttpPost, ActionName(name: nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _uow.IdentityRoleClaims.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(actionName: nameof(Index));
        }

    }
}
