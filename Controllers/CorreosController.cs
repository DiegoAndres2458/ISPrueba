using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ISPrueba.Models;

namespace ISPrueba.Controllers
{
    public class CorreosController : Controller
    {
        private readonly ISPruebaContext _context;

        public CorreosController(ISPruebaContext context)
        {
            _context = context;
        }

        // GET: Correos
        public async Task<IActionResult> Index()
        {
            var iSPruebaContext = _context.Correos.Include(c => c.IdPersonaNavigation);
            return View(await iSPruebaContext.ToListAsync());
        }

        // GET: Correos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Correos == null)
            {
                return NotFound();
            }

            var correo = await _context.Correos
                .Include(c => c.IdPersonaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (correo == null)
            {
                return NotFound();
            }

            return View(correo);
        }

        // GET: Correos/Create
        public IActionResult Create()
        {
            ViewData["IdPersona"] = new SelectList(_context.Personas, "Id", "Nombre");
            return View();
        }

        // POST: Correos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Correo1,IdPersona")] Correo correo)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(correo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["IdPersona"] = new SelectList(_context.Personas, "Id", "Id", correo.IdPersona);
            return View(correo);
        }

        // GET: Correos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Correos == null)
            {
                return NotFound();
            }

            var correo = await _context.Correos.FindAsync(id);
            if (correo == null)
            {
                return NotFound();
            }
            ViewData["IdPersona"] = new SelectList(_context.Personas, "Id", "Nombre", correo.IdPersona);
            return View(correo);
        }

        // POST: Correos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Correo1,IdPersona")] Correo correo)
        {
            if (id != correo.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(correo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CorreoExists(correo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["IdPersona"] = new SelectList(_context.Personas, "Id", "Id", correo.IdPersona);
            return View(correo);
        }

        // GET: Correos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Correos == null)
            {
                return NotFound();
            }

            var correo = await _context.Correos
                .Include(c => c.IdPersonaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (correo == null)
            {
                return NotFound();
            }

            return View(correo);
        }

        // POST: Correos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Correos == null)
            {
                return Problem("Entity set 'ISPruebaContext.Correos'  is null.");
            }
            var correo = await _context.Correos.FindAsync(id);
            if (correo != null)
            {
                _context.Correos.Remove(correo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CorreoExists(int id)
        {
          return _context.Correos.Any(e => e.Id == id);
        }
    }
}
