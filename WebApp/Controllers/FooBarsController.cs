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
            var foobars = await _uow.FooBars.AllForUserAsync(User.GetUserId<int>());
            return View(foobars);
        }

        //GET: FooBars/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(fooBar);
        }

        // GET: FooBars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FooBars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FooBar fooBar)
        {
            if (ModelState.IsValid)
            {
                fooBar.ApplicationUserId = User.GetUserId<int>();
                _uow.FooBars.Add(fooBar);
                await _uow.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(fooBar);
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
            return View(fooBar);
        }

        // POST: FooBars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FooBar fooBar)
        {
            if (id != fooBar.FooBarId)
            {
                return NotFound();
            }

            var dbFooBar = await _uow.FooBars.FindAsync(id);
            if (fooBar == null || dbFooBar.ApplicationUserId != User.GetUserId<int>())
            {
                return NotFound();
            }

            dbFooBar.IntValue = fooBar.IntValue;
            dbFooBar.StringValue = fooBar.StringValue;

            if (ModelState.IsValid)
            {

                try
                {
                    _uow.FooBars.Update(dbFooBar);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_uow.FooBars.Exists(fooBar.FooBarId))
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
            return View(fooBar);
        }

        // GET: FooBars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fooBar = await _uow.FooBars.FindAsync(id.Value);
            if (fooBar == null || fooBar.ApplicationUserId != User.GetUserId<int>())
            {
                return NotFound();
            }

            return View(fooBar);
        }

        // POST: FooBars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var fooBar = await _uow.FooBars.FindAsync(id);
            if (fooBar == null || fooBar.ApplicationUserId != User.GetUserId<int>())
            {
                return NotFound();
            }
            _uow.FooBars.Remove(fooBar);
            await _uow.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
