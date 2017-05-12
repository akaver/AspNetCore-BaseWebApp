using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.EntityFrameworkCore;
using Domain;

namespace WebApp.Areas.Culture.Controllers
{
    [Area("Culture")]
    public class TranslationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TranslationsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Culture/Translations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Translations.ToListAsync());
        }

        // GET: Culture/Translations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translation = await _context.Translations
                .SingleOrDefaultAsync(m => m.TranslationId == id);
            if (translation == null)
            {
                return NotFound();
            }

            return View(translation);
        }

        // GET: Culture/Translations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Culture/Translations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TranslationId,Value,Culture")] Translation translation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(translation);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(translation);
        }

        // GET: Culture/Translations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translation = await _context.Translations.SingleOrDefaultAsync(m => m.TranslationId == id);
            if (translation == null)
            {
                return NotFound();
            }
            return View(translation);
        }

        // POST: Culture/Translations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TranslationId,Value,Culture")] Translation translation)
        {
            if (id != translation.TranslationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(translation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TranslationExists(translation.TranslationId))
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
            return View(translation);
        }

        // GET: Culture/Translations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translation = await _context.Translations
                .SingleOrDefaultAsync(m => m.TranslationId == id);
            if (translation == null)
            {
                return NotFound();
            }

            return View(translation);
        }

        // POST: Culture/Translations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var translation = await _context.Translations.SingleOrDefaultAsync(m => m.TranslationId == id);
            _context.Translations.Remove(translation);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TranslationExists(int id)
        {
            return _context.Translations.Any(e => e.TranslationId == id);
        }
    }
}
