using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AspNetCore.Identity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize]
    public class FooBarsController : Controller
    {
        private readonly IApplicationUnitOfWork _uow;


        public FooBarsController(IApplicationUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: FooBars
        public async Task<IActionResult> Index()
        {
            // show only these items, that belong to the current user
            
            //// two ways to get id
            //// from user directly - it will be string
            //var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //// from usermanager - still string. and you have to di usermanager in constructor
            //var userId2 = _userManager.GetUserId(User);

            //so, extension method to simplify things
            //look in AspNetCore.Identity.Extensions ClaimsPrincipalExtension
            var foobars = await _uow.FooBars.AllForUserAsync(userId: User.GetUserId<int>());
            return View(model: foobars);
        }

        //GET: FooBars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fooBar = await _uow.FooBars.SingleOrDefaultIncludeNavigation(id: id.Value);
            if (fooBar == null || fooBar.ApplicationUserId != User.GetUserId<int>())
            {
                return NotFound();
            }

            return View(model: fooBar);
        }

        // GET: FooBars/Create
        public IActionResult Create()
        {
            var vm = new FooBarsCreateEditViewModel();
            vm.BlahOneSelectList = new SelectList(items: _uow.Blahs.All(), dataValueField: nameof(Blah.BlahId), dataTextField: nameof(Blah.BlahValue));
            vm.BlahTwoSelectList = new SelectList(items: _uow.Blahs.All(), dataValueField: nameof(Blah.BlahId), dataTextField: nameof(Blah.BlahValue));
            vm.BlahThreeSelectList = new SelectList(items: _uow.Blahs.All(), dataValueField: nameof(Blah.BlahId), dataTextField: nameof(Blah.BlahValue));

            return View(vm);
        }

        // POST: FooBars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FooBarsCreateEditViewModel vm)
        {
            if (ModelState.IsValid)
            {
                vm.FooBar.ApplicationUserId = User.GetUserId<int>();
                _uow.FooBars.Add(entity: vm.FooBar);
                await _uow.SaveChangesAsync();
                return RedirectToAction(actionName: nameof(Index));
            }

            vm.BlahOneSelectList = new SelectList(items: _uow.Blahs.All(), dataValueField: nameof(Blah.BlahId), dataTextField: nameof(Blah.BlahValue), selectedValue: vm.FooBar.BlahOneId);
            vm.BlahTwoSelectList = new SelectList(items: _uow.Blahs.All(), dataValueField: nameof(Blah.BlahId), dataTextField: nameof(Blah.BlahValue), selectedValue: vm.FooBar.BlahTwoId);
            vm.BlahThreeSelectList = new SelectList(items: _uow.Blahs.All(), dataValueField: nameof(Blah.BlahId), dataTextField: nameof(Blah.BlahValue), selectedValue: vm.FooBar.BlahThreeId);

            return View(model: vm);
        }

        // GET: FooBars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fooBar = await _uow.FooBars.FindAsync(id);
            if (fooBar == null || fooBar.ApplicationUserId != User.GetUserId<int>())
            {
                return NotFound();
            }

            var vm = new FooBarsCreateEditViewModel();
            vm.FooBar = fooBar;
            vm.BlahOneSelectList = new SelectList(items: _uow.Blahs.All(), dataValueField: nameof(Blah.BlahId), dataTextField: nameof(Blah.BlahValue), selectedValue: vm.FooBar.BlahOneId);
            vm.BlahTwoSelectList = new SelectList(items: _uow.Blahs.All(), dataValueField: nameof(Blah.BlahId), dataTextField: nameof(Blah.BlahValue), selectedValue: vm.FooBar.BlahTwoId);
            vm.BlahThreeSelectList = new SelectList(items: _uow.Blahs.All(), dataValueField: nameof(Blah.BlahId), dataTextField: nameof(Blah.BlahValue), selectedValue: vm.FooBar.BlahThreeId);

            return View(model: vm);
        }

        // POST: FooBars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FooBarsCreateEditViewModel vm)
        {
            if (id != vm.FooBar.FooBarId)
            {
                return NotFound();
            }

            var dbFooBar = await _uow.FooBars.FindAsync(id);
            if (vm.FooBar == null || dbFooBar.ApplicationUserId != User.GetUserId<int>())
            {
                return NotFound();
            }

            dbFooBar.IntValue = vm.FooBar.IntValue;
            dbFooBar.StringValue = vm.FooBar.StringValue;
            dbFooBar.BlahOneId = vm.FooBar.BlahThreeId;
            dbFooBar.BlahTwoId = vm.FooBar.BlahThreeId;
            dbFooBar.BlahThreeId = vm.FooBar.BlahThreeId;

            if (ModelState.IsValid)
            {

                try
                {
                    _uow.FooBars.Update(entity: dbFooBar);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_uow.FooBars.Exists(id: dbFooBar.FooBarId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(actionName: "Index");
            }

            vm.BlahOneSelectList = new SelectList(items: _uow.Blahs.All(), dataValueField: nameof(Blah.BlahId), dataTextField: nameof(Blah.BlahValue), selectedValue: vm.FooBar.BlahOneId);
            vm.BlahTwoSelectList = new SelectList(items: _uow.Blahs.All(), dataValueField: nameof(Blah.BlahId), dataTextField: nameof(Blah.BlahValue), selectedValue: vm.FooBar.BlahTwoId);
            vm.BlahThreeSelectList = new SelectList(items: _uow.Blahs.All(), dataValueField: nameof(Blah.BlahId), dataTextField: nameof(Blah.BlahValue), selectedValue: vm.FooBar.BlahThreeId);

            return View(model: vm);
        }

        // GET: FooBars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fooBar = await _uow.FooBars.SingleOrDefaultIncludeNavigation(id: id.Value);

            if (fooBar == null || fooBar.ApplicationUserId != User.GetUserId<int>())
            {
                return NotFound();
            }

            return View(model: fooBar);
        }

        // POST: FooBars/Delete/5
        [HttpPost, ActionName(name: "Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var fooBar = await _uow.FooBars.FindAsync(id);
            if (fooBar == null || fooBar.ApplicationUserId != User.GetUserId<int>())
            {
                return NotFound();
            }
            _uow.FooBars.Remove(entity: fooBar);
            await _uow.SaveChangesAsync();

            return RedirectToAction(actionName: "Index");
        }

    }
}
