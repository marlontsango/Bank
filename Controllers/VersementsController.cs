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
    public class VersementsController : Controller
    {
        private readonly DataBaseContext _context;

        public VersementsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Versements
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.versement.Include(v => v.compte);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Versements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var versement = await _context.versement
                .Include(v => v.compte)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (versement == null)
            {
                return NotFound();
            }

            return View(versement);
        }

        // GET: Versements/Create
        public IActionResult Create()
        {
            ViewData["CompteId"] = new SelectList(_context.compte, "Id", "Id");
            return View();
        }

        // POST: Versements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,motif,date_versement,montant_versement,CompteId")] Versement versement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(versement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompteId"] = new SelectList(_context.compte, "Id", "Id", versement.CompteId);
            return View(versement);
        }

        // GET: Versements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var versement = await _context.versement.FindAsync(id);
            if (versement == null)
            {
                return NotFound();
            }
            ViewData["CompteId"] = new SelectList(_context.compte, "Id", "Id", versement.CompteId);
            return View(versement);
        }

        // POST: Versements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,motif,date_versement,montant_versement,CompteId")] Versement versement)
        {
            if (id != versement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(versement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VersementExists(versement.Id))
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
            ViewData["CompteId"] = new SelectList(_context.compte, "Id", "Id", versement.CompteId);
            return View(versement);
        }

        // GET: Versements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var versement = await _context.versement
                .Include(v => v.compte)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (versement == null)
            {
                return NotFound();
            }

            return View(versement);
        }

        // POST: Versements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var versement = await _context.versement.FindAsync(id);
            _context.versement.Remove(versement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VersementExists(int id)
        {
            return _context.versement.Any(e => e.Id == id);
        }
    }
}
