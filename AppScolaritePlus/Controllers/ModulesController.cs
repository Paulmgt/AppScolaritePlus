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
    public class ModulesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _webHostEnvironment;

        public ModulesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Modules
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Modules.Include(m => m.Parcours).Include(m => m.UnitePedagogique);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Modules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var modules = await _context.Modules
                .Include(m => m.Parcours)
                .Include(m => m.UnitePedagogique)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modules == null)
            {
                return NotFound();
            }

            return View(modules);
        }

        // GET: Modules/Create
        public IActionResult Create()
        {
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Intitule");
            ViewData["UnitesPedagogiqueId"] = new SelectList(_context.UnitesPedagogiques, "Id", "Designation");
            return View();
        }

        // POST: Modules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Descriptif,Logo,Resume,Infos,ParcoursId,UnitesPedagogiqueId")] Modules modules, IFormFile Logo)
        {
            if (Logo.Length > 0)
                if (ModelState.IsValid)
            {
                    string rootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(Logo.FileName) + "_" +
                               Guid.NewGuid() +
                               Path.GetExtension(Logo.FileName);
                    string path = Path.Combine(rootPath + "/LogosModules/", fileName);
                    // copier physiquement le fichier dans les dossier Images
                    // pour cela on utilise le fileStream

                    var fileStream = new FileStream(path, FileMode.Create);
                    // copi async

                    await Logo.CopyToAsync(fileStream);

                    fileStream.Close();
                    modules.Logo = fileName;

                    _context.Add(modules);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Intitule", modules.ParcoursId);
            ViewData["UnitesPedagogiqueId"] = new SelectList(_context.UnitesPedagogiques, "Id", "Designation", modules.UnitesPedagogiqueId);
            return View(modules);
        }

        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var modules = await _context.Modules.FindAsync(id);
            if (modules == null)
            {
                return NotFound();
            }
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Intitule", modules.ParcoursId);
            ViewData["UnitesPedagogiqueId"] = new SelectList(_context.UnitesPedagogiques, "Id", "Designation", modules.UnitesPedagogiqueId);
            return View(modules);
        }

        // POST: Modules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Descriptif,Logo,Resume,Infos,ParcoursId,UnitesPedagogiqueId")] Modules modules, IFormFile Logo)
        {
            if (id != modules.Id)
            {
                return NotFound();
            }

            if (Logo.Length > 0)
                if (ModelState.IsValid)
            {
                try
                {

                    string rootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(Logo.FileName) + "_" +
                               Guid.NewGuid() +
                               Path.GetExtension(Logo.FileName);
                    string path = Path.Combine(rootPath + "/LogosModules/", fileName);
                    // copier physiquement le fichier dans les dossier Images
                    // pour cela on utilise le fileStream

                    var fileStream = new FileStream(path, FileMode.Create);
                    // copi async

                    await Logo.CopyToAsync(fileStream);

                    fileStream.Close();
                    modules.Logo = fileName;

                    _context.Update(modules);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModulesExists(modules.Id))
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
            ViewData["ParcoursId"] = new SelectList(_context.Parcours, "Id", "Intitule", modules.ParcoursId);
            ViewData["UnitesPedagogiqueId"] = new SelectList(_context.UnitesPedagogiques, "Id", "Designation", modules.UnitesPedagogiqueId);
            return View(modules);
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Modules == null)
            {
                return NotFound();
            }

            var modules = await _context.Modules
                .Include(m => m.Parcours)
                .Include(m => m.UnitePedagogique)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modules == null)
            {
                return NotFound();
            }

            return View(modules);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Modules == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Modules'  is null.");
            }
            var modules = await _context.Modules.FindAsync(id);
            if (modules != null)
            {
                _context.Modules.Remove(modules);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModulesExists(int id)
        {
          return (_context.Modules?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
