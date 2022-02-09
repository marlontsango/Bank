#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bank.DataBase;
using Bank.Models;

namespace Bank.Controllers
{
    public class RetraitsController : Controller
    {
        private readonly DataBaseContext _context;

        public RetraitsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Retraits
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.retrait.Include(r => r.compte);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Retraits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retrait = await _context.retrait
                .Include(r => r.compte)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (retrait == null)
            {
                return NotFound();
            }

            return View(retrait);
        }

        // GET: Retraits/Create
        public IActionResult Create()
        {
            ViewData["CompteId"] = new SelectList(_context.compte, "Id", "Id");
            return View();
        }

        // POST: Retraits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,motif_retrait,date_retrait,montant_retrait,CompteId")] Retrait retrait)
        {
            if (ModelState.IsValid)
            {
                _context.Add(retrait);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompteId"] = new SelectList(_context.compte, "Id", "Id", retrait.CompteId);
            return View(retrait);
        }

        // GET: Retraits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retrait = await _context.retrait.FindAsync(id);
            if (retrait == null)
            {
                return NotFound();
            }
            ViewData["CompteId"] = new SelectList(_context.compte, "Id", "Id", retrait.CompteId);
            return View(retrait);
        }

        // POST: Retraits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,motif_retrait,date_retrait,montant_retrait,CompteId")] Retrait retrait)
        {
            if (id != retrait.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(retrait);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RetraitExists(retrait.Id))
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
            ViewData["CompteId"] = new SelectList(_context.compte, "Id", "Id", retrait.CompteId);
            return View(retrait);
        }

        // GET: Retraits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var retrait = await _context.retrait
                .Include(r => r.compte)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (retrait == null)
            {
                return NotFound();
            }

            return View(retrait);
        }

        // POST: Retraits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var retrait = await _context.retrait.FindAsync(id);
            _context.retrait.Remove(retrait);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RetraitExists(int id)
        {
            return _context.retrait.Any(e => e.Id == id);
        }
    }
}
