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
    public class MultiLangStringsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MultiLangStringsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Culture/MultiLangStrings
        public async Task<IActionResult> Index()
        {
            return View(await _context.MultiLangStrings.ToListAsync());
        }

        // GET: Culture/MultiLangStrings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var multiLangString = await _context.MultiLangStrings
                .SingleOrDefaultAsync(m => m.MultiLangStringId == id);
            if (multiLangString == null)
            {
                return NotFound();
            }

            return View(multiLangString);
        }

        // GET: Culture/MultiLangStrings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Culture/MultiLangStrings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MultiLangStringId,Value,Owner")] MultiLangString multiLangString)
        {
            if (ModelState.IsValid)
            {
                _context.Add(multiLangString);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(multiLangString);
        }

        // GET: Culture/MultiLangStrings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var multiLangString = await _context.MultiLangStrings.SingleOrDefaultAsync(m => m.MultiLangStringId == id);
            if (multiLangString == null)
            {
                return NotFound();
            }
            return View(multiLangString);
        }

        // POST: Culture/MultiLangStrings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MultiLangStringId,Value,Owner")] MultiLangString multiLangString)
        {
            if (id != multiLangString.MultiLangStringId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(multiLangString);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MultiLangStringExists(multiLangString.MultiLangStringId))
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
            return View(multiLangString);
        }

        // GET: Culture/MultiLangStrings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var multiLangString = await _context.MultiLangStrings
                .SingleOrDefaultAsync(m => m.MultiLangStringId == id);
            if (multiLangString == null)
            {
                return NotFound();
            }

            return View(multiLangString);
        }

        // POST: Culture/MultiLangStrings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var multiLangString = await _context.MultiLangStrings.SingleOrDefaultAsync(m => m.MultiLangStringId == id);
            _context.MultiLangStrings.Remove(multiLangString);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MultiLangStringExists(int id)
        {
            return _context.MultiLangStrings.Any(e => e.MultiLangStringId == id);
        }
    }
}
