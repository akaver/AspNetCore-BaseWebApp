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

namespace WebApp.Controllers
{
    public class BlahsController : Controller
    {
        private readonly IApplicationUnitOfWork _uow;

        public BlahsController(IApplicationUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: Blahs
        public async Task<IActionResult> Index()
        {
            return View(await _uow.Blahs.AllAsync());
        }

        // GET: Blahs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blah = await _uow.Blahs.FindAsync(id);

            if (blah == null)
            {
                return NotFound();
            }

            return View(blah);
        }

        // GET: Blahs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blahs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blah blah)
        {
            if (ModelState.IsValid)
            {
                _uow.Blahs.Add(blah);
                await _uow.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blah);
        }

        // GET: Blahs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blah = await _uow.Blahs.FindAsync(id);
            if (blah == null)
            {
                return NotFound();
            }
            return View(blah);
        }

        // POST: Blahs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Blah blah)
        {
            if (id != blah.BlahId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.Blahs.Update(blah);
                    await _uow.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_uow.Blahs.Exists(blah.BlahId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blah);
        }

        // GET: Blahs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blah = await _uow.Blahs.FindAsync(id);
            if (blah == null)
            {
                return NotFound();
            }

            return View(blah);
        }

        // POST: Blahs/Delete/5
        [HttpPost, ActionName(nameof(Delete))]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _uow.Blahs.Remove(id);
            await _uow.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
