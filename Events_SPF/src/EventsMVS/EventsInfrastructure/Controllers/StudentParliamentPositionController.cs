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
    public class StudentParliamentPositionController : Controller
    {
        private readonly BdeventsContext _context;

        public StudentParliamentPositionController(BdeventsContext context)
        {
            _context = context;
        }

        // GET: StudentParliamentPosition
        public async Task<IActionResult> Index()
        {
            return View(await _context.StudentParliamentPositions.ToListAsync());
        }

        // GET: StudentParliamentPositions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var position = await _context.StudentParliamentPositions
                .Include(p => p.StudentParliamentMembers)
                .ThenInclude(spm => spm.Student)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }


        // GET: StudentParliamentPosition/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StudentParliamentPosition/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] StudentParliamentPosition studentParliamentPosition)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentParliamentPosition);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(studentParliamentPosition);
        }

        // GET: StudentParliamentPosition/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentParliamentPosition = await _context.StudentParliamentPositions.FindAsync(id);
            if (studentParliamentPosition == null)
            {
                return NotFound();
            }
            return View(studentParliamentPosition);
        }

        // POST: StudentParliamentPosition/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] StudentParliamentPosition studentParliamentPosition)
        {
            if (id != studentParliamentPosition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentParliamentPosition);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentParliamentPositionExists(studentParliamentPosition.Id))
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
            return View(studentParliamentPosition);
        }

        // GET: StudentParliamentPosition/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentParliamentPosition = await _context.StudentParliamentPositions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentParliamentPosition == null)
            {
                return NotFound();
            }

            return View(studentParliamentPosition);
        }

        // POST: StudentParliamentPosition/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentParliamentPosition = await _context.StudentParliamentPositions.FindAsync(id);
            if (studentParliamentPosition != null)
            {
                _context.StudentParliamentPositions.Remove(studentParliamentPosition);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentParliamentPositionExists(int id)
        {
            return _context.StudentParliamentPositions.Any(e => e.Id == id);
        }
    }
}
