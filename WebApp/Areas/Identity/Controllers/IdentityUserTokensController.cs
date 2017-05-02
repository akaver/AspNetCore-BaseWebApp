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
using Microsoft.AspNetCore.Authorization;
using WebApp.Areas.Identity.ViewModels;

namespace WebApp.Areas.Identity.Controllers
{
    [Area(areaName: "Identity")]
    [Authorize(Roles = "Admin")]
    public class IdentityUserTokensController : Controller
    {
        private readonly IIdentityUnitOfWork _uow;

        public IdentityUserTokensController(IIdentityUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Identity/IdentityUserTokens
        public async Task<IActionResult> Index()
        {
            var userTokens = await _uow.IdentityUserTokens.AllIncludeUserAsync();
            return View(model: userTokens);
        }

        // GET: Identity/IdentityUserTokens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserToken = await _uow.IdentityUserTokens.SingleByIdIncludeUserAsync(id: id.Value);
            if (identityUserToken == null)
            {
                return NotFound();
            }

            return View(model: identityUserToken);
        }

        // GET: Identity/IdentityUserTokens/Create
        public IActionResult Create()
        {

            var vm = new IdentityUserTokensCreateEditViewModel();
            vm.UserSelectList = new SelectList(items: _uow.IdentityUsers.All(), dataValueField: nameof(IdentityUser.IdentityUserId), dataTextField: nameof(IdentityUser.Email));
            return View(model: vm);
        }

        // POST: Identity/IdentityUserTokens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityUserTokensCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.IdentityUserTokens.Add(entity: vm.IdentityUserToken);
                await _uow.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            vm.UserSelectList = new SelectList(items: _uow.IdentityUsers.All(), dataValueField: nameof(IdentityUser.IdentityUserId), dataTextField: nameof(IdentityUser.Email), selectedValue: vm.IdentityUserToken.UserId);
            return View(model: vm);
        }

        // GET: Identity/IdentityUserTokens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserToken = await _uow.IdentityUserTokens.SingleByIdIncludeUserAsync(id: id.Value);
            if (identityUserToken == null)
            {
                return NotFound();
            }
            var vm = new IdentityUserTokensCreateEditViewModel();
            vm.IdentityUserToken = identityUserToken;
            vm.UserSelectList = new SelectList(items: _uow.IdentityUsers.All(), dataValueField: nameof(IdentityUser.IdentityUserId), dataTextField: nameof(IdentityUser.Email), selectedValue: identityUserToken.UserId);
            return View(model: vm);
        }

        // POST: Identity/IdentityUserTokens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IdentityUserTokensCreateEditViewModel vm)
        {
            if (id != vm.IdentityUserToken.IdentityUserTokenId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.IdentityUserTokens.Update(entity: vm.IdentityUserToken);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_uow.IdentityUserTokens.Exists(id: vm.IdentityUserToken.IdentityUserTokenId))
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
            vm.UserSelectList = new SelectList(items: _uow.IdentityUsers.All(), dataValueField: nameof(IdentityUser.IdentityUserId), dataTextField: nameof(IdentityUser.Email), selectedValue: vm.IdentityUserToken.UserId);

            return View(model: vm);
        }

        // GET: Identity/IdentityUserTokens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserToken = await _uow.IdentityUserTokens.SingleByIdIncludeUserAsync(id: id.Value);
            if (identityUserToken == null)
            {
                return NotFound();
            }

            return View(model: identityUserToken);
        }

        // POST: Identity/IdentityUserTokens/Delete/5
        [HttpPost, ActionName(name: nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _uow.IdentityUserTokens.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(actionName: nameof(Index));
        }
    }
}
