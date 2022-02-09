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
    public class VirementsController : Controller
    {
        private readonly DataBaseContext _context;

        public VirementsController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Virements
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.virement.Include(v => v.compte);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Virements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var virement = await _context.virement
                .Include(v => v.compte)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (virement == null)
            {
                return NotFound();
            }

            return View(virement);
        }

        // GET: Virements/Create
        public IActionResult Create()
        {
            ViewData["CompteId"] = new SelectList(_context.compte, "Id", "Id");
            return View();
        }

        // POST: Virements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DateVirement,montant_virement,motif_virement,CompteId")] Virement virement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(virement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompteId"] = new SelectList(_context.compte, "Id", "Id", virement.CompteId);
            return View(virement);
        }

        // GET: Virements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var virement = await _context.virement.FindAsync(id);
            if (virement == null)
            {
                return NotFound();
            }
            ViewData["CompteId"] = new SelectList(_context.compte, "Id", "Id", virement.CompteId);
            return View(virement);
        }

        // POST: Virements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DateVirement,montant_virement,motif_virement,CompteId")] Virement virement)
        {
            if (id != virement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(virement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VirementExists(virement.Id))
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
            ViewData["CompteId"] = new SelectList(_context.compte, "Id", "Id", virement.CompteId);
            return View(virement);
        }

        // GET: Virements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var virement = await _context.virement
                .Include(v => v.compte)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (virement == null)
            {
                return NotFound();
            }

            return View(virement);
        }

        // POST: Virements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var virement = await _context.virement.FindAsync(id);
            _context.virement.Remove(virement);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VirementExists(int id)
        {
            return _context.virement.Any(e => e.Id == id);
        }
    }
}
