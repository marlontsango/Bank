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
    public class ComptesController : Controller
    {
        private readonly DataBaseContext _context;

        public ComptesController(DataBaseContext context)
        {
            _context = context;
        }

        // GET: Comptes
        public async Task<IActionResult> Index()
        {
            var dataBaseContext = _context.compte.Include(c => c.client).Include(c => c.type);
            return View(await dataBaseContext.ToListAsync());
        }

        // GET: Comptes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compte = await _context.compte
                .Include(c => c.client)
                .Include(c => c.type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compte == null)
            {
                return NotFound();
            }

            return View(compte);
        }

        // GET: Comptes/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.client, "Id", "Id");
            ViewData["TypeId"] = new SelectList(_context.type, "Id", "Id");
            return View();
        }

        // POST: Comptes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,intitule_compte,date_ouverture,solde,numero_compte,ClientId,TypeId")] Compte compte)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compte);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_context.client, "Id", "Id", compte.ClientId);
            ViewData["TypeId"] = new SelectList(_context.type, "Id", "Id", compte.TypeId);
            return View(compte);
        }

        // GET: Comptes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compte = await _context.compte.FindAsync(id);
            if (compte == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.client, "Id", "Id", compte.ClientId);
            ViewData["TypeId"] = new SelectList(_context.type, "Id", "Id", compte.TypeId);
            return View(compte);
        }

        // POST: Comptes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,intitule_compte,date_ouverture,solde,numero_compte,ClientId,TypeId")] Compte compte)
        {
            if (id != compte.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compte);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompteExists(compte.Id))
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
            ViewData["ClientId"] = new SelectList(_context.client, "Id", "Id", compte.ClientId);
            ViewData["TypeId"] = new SelectList(_context.type, "Id", "Id", compte.TypeId);
            return View(compte);
        }

        // GET: Comptes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compte = await _context.compte
                .Include(c => c.client)
                .Include(c => c.type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compte == null)
            {
                return NotFound();
            }

            return View(compte);
        }

        // POST: Comptes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compte = await _context.compte.FindAsync(id);
            _context.compte.Remove(compte);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompteExists(int id)
        {
            return _context.compte.Any(e => e.Id == id);
        }
    }
}
