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
    public class IdentityUserClaimsController : Controller
    {
        private readonly IIdentityUnitOfWork _uow;

        public IdentityUserClaimsController(IIdentityUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Identity/IdentityUserClaims
        public async Task<IActionResult> Index()
        {
            var userClaims = await _uow.IdentityUserClaims.AllIncludeUserAsync();
            return View(model: userClaims);
        }

        // GET: Identity/IdentityUserClaims/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserClaim = await _uow.IdentityUserClaims.SingleByIdIncludeUserAsync(id: id.Value);
            if (identityUserClaim == null)
            {
                return NotFound();
            }

            return View(model: identityUserClaim);
        }

        // GET: Identity/IdentityUserClaims/Create
        public IActionResult Create()
        {
            var vm = new IdentityUserClaimsCreateEditViewModel();
            vm.UserSelectList = new SelectList(
                items: _uow.IdentityUsers.All(), 
                dataValueField: nameof(IdentityUser.IdentityUserId), 
                dataTextField: nameof(IdentityUser.Email));
            return View(model: vm);
        }

        // POST: Identity/IdentityUserClaims/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityUserClaimsCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                _uow.IdentityUserClaims.Add(entity: vm.IdentityUserClaim);
                await _uow.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }
            vm.UserSelectList = new SelectList(
                items: _uow.IdentityUsers.All(), 
                dataValueField: nameof(IdentityUser.IdentityUserId), 
                dataTextField: nameof(IdentityUser.Email), 
                selectedValue: vm.IdentityUserClaim.UserId);
            return View(model: vm);
        }

        // GET: Identity/IdentityUserClaims/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserClaim = await _uow.IdentityUserClaims.SingleByIdIncludeUserAsync(id: id.Value);
            if (identityUserClaim == null)
            {
                return NotFound();
            }
            var vm = new IdentityUserClaimsCreateEditViewModel()
            {
                IdentityUserClaim = identityUserClaim,
                UserSelectList = new SelectList(
                    items: _uow.IdentityUsers.All(), 
                    dataValueField: nameof(IdentityUser.IdentityUserId), 
                    dataTextField: nameof(IdentityUser.Email), 
                    selectedValue: identityUserClaim.UserId)
            };
            return View(model: vm);
        }

        // POST: Identity/IdentityUserClaims/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IdentityUserClaimsCreateEditViewModel vm)
        {
            if (id != vm.IdentityUserClaim.IdentityUserClaimId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.IdentityUserClaims.Update(entity: vm.IdentityUserClaim);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_uow.IdentityUserClaims.Exists(id: vm.IdentityUserClaim.IdentityUserClaimId))
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
                selectedValue: vm.IdentityUserClaim.UserId);
            return View(model: vm);
        }

        // GET: Identity/IdentityUserClaims/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityUserClaim = await _uow.IdentityUserClaims.SingleByIdIncludeUserAsync(id: id.Value);
            if (identityUserClaim == null)
            {
                return NotFound();
            }

            return View(model: identityUserClaim);
        }

        // POST: Identity/IdentityUserClaims/Delete/5
        [HttpPost, ActionName(name: nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _uow.IdentityUserClaims.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(actionName: nameof(Index));
        }

    }
}
