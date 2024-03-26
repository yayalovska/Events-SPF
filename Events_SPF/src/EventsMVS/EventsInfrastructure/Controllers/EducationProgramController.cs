using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventsDomain.Models;
using EventsInfrastructure;

namespace EventsInfrastructure.Controllers
{
    public class EducationProgramController : Controller
    {
        private readonly BdeventsContext _context;

        public EducationProgramController(BdeventsContext context)
        {
            _context = context;
        }

        // GET: EducationProgram
        public async Task<IActionResult> Index()
        {
            var bdeventsContext = _context.EducationPrograms.Include(e => e.Faculty);
            return View(await bdeventsContext.ToListAsync());
        }

        // GET: EducationProgram/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationProgram = await _context.EducationPrograms
                .Include(e => e.Faculty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationProgram == null)
            {
                return NotFound();
            }

            return View(educationProgram);
        }

        // GET: EducationProgram/Create
        public IActionResult Create()
        {
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name");
            return View();
        }

        // POST: EducationProgram/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,FacultyId")] EducationProgram educationProgram)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educationProgram);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", educationProgram.FacultyId);
            return View(educationProgram);
        }

        // GET: EducationProgram/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationProgram = await _context.EducationPrograms.FindAsync(id);
            if (educationProgram == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", educationProgram.FacultyId);
            return View(educationProgram);
        }

        // POST: EducationProgram/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,FacultyId")] EducationProgram educationProgram)
        {
            if (id != educationProgram.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educationProgram);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationProgramExists(educationProgram.Id))
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
            ViewData["FacultyId"] = new SelectList(_context.Faculties, "Id", "Name", educationProgram.FacultyId);
            return View(educationProgram);
        }

        // GET: EducationProgram/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationProgram = await _context.EducationPrograms
                .Include(e => e.Faculty)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationProgram == null)
            {
                return NotFound();
            }

            return View(educationProgram);
        }

        // POST: EducationProgram/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var educationProgram = await _context.EducationPrograms.FindAsync(id);
            if (educationProgram != null)
            {
                _context.EducationPrograms.Remove(educationProgram);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EducationProgramExists(int id)
        {
            return _context.EducationPrograms.Any(e => e.Id == id);
        }
    }
}
