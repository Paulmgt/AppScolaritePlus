using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppScolaritePlus.Models;

namespace AppScolaritePlus.Controllers
{
    public class SessionsFormationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SessionsFormationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SessionsFormations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SessionsFormations.Include(s => s.Parcours);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SessionsFormations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SessionsFormations == null)
            {
                return NotFound();
            }

            var sessionsFormation = await _context.SessionsFormations
                .Include(s => s.Parcours)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sessionsFormation == null)
            {
                return NotFound();
            }

            return View(sessionsFormation);
        }

        // GET: SessionsFormations/Create
        public IActionResult Create()
        {
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Id");
            return View();
        }

        // POST: SessionsFormations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,DateDebut,DateFin,ParcoursId")] SessionsFormation sessionsFormation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sessionsFormation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Intitule", sessionsFormation.ParcoursId);
            return View(sessionsFormation);
        }

        // GET: SessionsFormations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SessionsFormations == null)
            {
                return NotFound();
            }

            var sessionsFormation = await _context.SessionsFormations.FindAsync(id);
            if (sessionsFormation == null)
            {
                return NotFound();
            }
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Intitule", sessionsFormation.ParcoursId);
            return View(sessionsFormation);
        }

        // POST: SessionsFormations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,DateDebut,DateFin,ParcoursId")] SessionsFormation sessionsFormation)
        {
            if (id != sessionsFormation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sessionsFormation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionsFormationExists(sessionsFormation.Id))
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
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Intitule", sessionsFormation.ParcoursId);
            return View(sessionsFormation);
        }

        // GET: SessionsFormations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SessionsFormations == null)
            {
                return NotFound();
            }

            var sessionsFormation = await _context.SessionsFormations
                .Include(s => s.Parcours)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sessionsFormation == null)
            {
                return NotFound();
            }

            return View(sessionsFormation);
        }

        // POST: SessionsFormations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SessionsFormations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SessionsFormations'  is null.");
            }
            var sessionsFormation = await _context.SessionsFormations.FindAsync(id);
            if (sessionsFormation != null)
            {
                _context.SessionsFormations.Remove(sessionsFormation);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SessionsFormationExists(int id)
        {
          return (_context.SessionsFormations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
