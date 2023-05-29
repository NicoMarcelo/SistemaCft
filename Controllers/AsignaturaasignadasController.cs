using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemaCft.Models;

namespace SistemaCft.Controllers
{
    public class AsignaturaasignadasController : Controller
    {
        private readonly SistemacftContext _context;

        public AsignaturaasignadasController(SistemacftContext context)
        {
            _context = context;
        }

        // GET: Asignaturaasignadas
        public async Task<IActionResult> Index()
        {
            var sistemacftContext = _context.Asignaturaasignada.Include(a => a.Asignatura).Include(a => a.Estudiante);
            return View(await sistemacftContext.ToListAsync());
        }

        // GET: Asignaturaasignadas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Asignaturaasignada == null)
            {
                return NotFound();
            }

            var asignaturaasignada = await _context.Asignaturaasignada
                .Include(a => a.Asignatura)
                .Include(a => a.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignaturaasignada == null)
            {
                return NotFound();
            }

            return View(asignaturaasignada);
        }

        // GET: Asignaturaasignadas/Create
        public IActionResult Create()
        {
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id");
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id");
            return View();
        }

        // POST: Asignaturaasignadas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AsignaturaId,EstudianteId,FechaRegistro")] Asignaturaasignada asignaturaasignada)
        {
            if (asignaturaasignada.EstudianteId != 0 && asignaturaasignada.AsignaturaId != 0)
            {
                _context.Asignaturaasignada.Add(asignaturaasignada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturaasignada.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturaasignada.EstudianteId);
            return View(asignaturaasignada);
        }

        // GET: Asignaturaasignadas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Asignaturaasignada == null)
            {
                return NotFound();
            }

            var asignaturaasignada = await _context.Asignaturaasignada.FindAsync(id);
            if (asignaturaasignada == null)
            {
                return NotFound();
            }
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturaasignada.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturaasignada.EstudianteId);
            return View(asignaturaasignada);
        }

        // POST: Asignaturaasignadas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AsignaturaId,EstudianteId,FechaRegistro")] Asignaturaasignada asignaturaasignada)
        {
            if (id != asignaturaasignada.Id)
            {
                return NotFound();
            }
                
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignaturaasignada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignaturaasignadaExists(asignaturaasignada.Id))
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
            ViewData["AsignaturaId"] = new SelectList(_context.Asignaturas, "Id", "Id", asignaturaasignada.AsignaturaId);
            ViewData["EstudianteId"] = new SelectList(_context.Estudiantes, "Id", "Id", asignaturaasignada.EstudianteId);
            return View(asignaturaasignada);
        }

        // GET: Asignaturaasignadas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Asignaturaasignada == null)
            {
                return NotFound();
            }

            var asignaturaasignada = await _context.Asignaturaasignada
                .Include(a => a.Asignatura)
                .Include(a => a.Estudiante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (asignaturaasignada == null)
            {
                return NotFound();
            }

            return View(asignaturaasignada);
        }

        // POST: Asignaturaasignadas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Asignaturaasignada == null)
            {
                return Problem("Entity set 'SistemacftContext.Asignaturaasignada'  is null.");
            }
            var asignaturaasignada = await _context.Asignaturaasignada.FindAsync(id);
            if (asignaturaasignada != null)
            {
                _context.Asignaturaasignada.Remove(asignaturaasignada);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignaturaasignadaExists(int id)
        {
          return (_context.Asignaturaasignada?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
