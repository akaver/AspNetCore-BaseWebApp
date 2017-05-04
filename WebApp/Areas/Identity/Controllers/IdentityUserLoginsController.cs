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
    public class IdentityUserLoginsController : Controller
    {
        private readonly IIdentityUnitOfWork<ApplicationUser> _uow;

        public IdentityUserLoginsController(IIdentityUnitOfWork<ApplicationUser> uow)
        {
            _uow = uow;
        }

        // GET: Identity/IdentityUserLogins
        public async Task<IActionResult> Index()
        {
            var userLogins = await _uow.IdentityUserLogins.AllIncludeUserAsync();
            return View(model: userLogins);
        }

        // GET: Identity/IdentityUserLogins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserLogin = await _uow.IdentityUserLogins.SingleByIdIncludeUserAsync(id: id.Value);
            if (identityUserLogin == null)
            {
                return NotFound();
            }

            return View(model: identityUserLogin);
        }

        // GET: Identity/IdentityUserLogins/Create
        public IActionResult Create()
        {
            var vm = new IdentityUserLoginsCreateEditViewModel();
            vm.UserSelectList = new SelectList(items: _uow.IdentityUsers.All(), dataValueField: nameof(IdentityUser.IdentityUserId), dataTextField: nameof(IdentityUser.Email));
            return View(model: vm);
        }

        // POST: Identity/IdentityUserLogins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityUserLoginsCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.IdentityUserLogins.Add(entity: vm.IdentityUserLogin);
                await _uow.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            vm.UserSelectList = new SelectList(items: _uow.IdentityUsers.All(), dataValueField: nameof(IdentityUser.IdentityUserId), dataTextField: nameof(IdentityUser.Email), selectedValue: vm.IdentityUserLogin.UserId);
            return View(model: vm);
        }

        // GET: Identity/IdentityUserLogins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserLogin = await _uow.IdentityUserLogins.SingleByIdIncludeUserAsync(id: id.Value);
            if (identityUserLogin == null)
            {
                return NotFound();
            }
            var vm = new IdentityUserLoginsCreateEditViewModel();
            vm.IdentityUserLogin = identityUserLogin;
            vm.UserSelectList = new SelectList(
                items: _uow.IdentityUsers.All(), 
                dataValueField: nameof(IdentityUser.IdentityUserId), 
                dataTextField: nameof(IdentityUser.Email), 
                selectedValue: vm.IdentityUserLogin.UserId);
            return View(model: vm);
        }

        // POST: Identity/IdentityUserLogins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IdentityUserLoginsCreateEditViewModel vm)
        {
            if (id != vm.IdentityUserLogin.IdentityUserLoginId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.IdentityUserLogins.Update(entity: vm.IdentityUserLogin);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_uow.IdentityUserLogins.Exists(id: vm.IdentityUserLogin.IdentityUserLoginId))
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
            vm.UserSelectList = new SelectList(
                items: _uow.IdentityUsers.All(),
                dataValueField: nameof(IdentityUser.IdentityUserId),
                dataTextField: nameof(IdentityUser.Email),
                selectedValue: vm.IdentityUserLogin.UserId);
            return View(model: vm);
        }

        // GET: Identity/IdentityUserLogins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserLogin = await _uow.IdentityUserLogins.SingleByIdIncludeUserAsync(id: id.Value);
            if (identityUserLogin == null)
            {
                return NotFound();
            }

            return View(model: identityUserLogin);
        }

        // POST: Identity/IdentityUserLogins/Delete/5
        [HttpPost, ActionName(name: nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _uow.IdentityUserLogins.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(actionName: nameof(Index));
        }

    }
}
