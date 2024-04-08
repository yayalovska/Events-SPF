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
    public class StudentParliamentMemberController : Controller
    {
        private readonly BdeventsContext _context;

        public StudentParliamentMemberController(BdeventsContext context)
        {
            _context = context;
        }

        // GET: StudentParliamentMember
        public async Task<IActionResult> Index()
        {
            var bdeventsContext = _context.StudentParliamentMembers.Include(s => s.Position).Include(s => s.Student);
            return View(await bdeventsContext.ToListAsync());
        }

        // GET: StudentParliamentMember/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentParliamentMember = await _context.StudentParliamentMembers
                .Include(s => s.Position)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentParliamentMember == null)
            {
                return NotFound();
            }

            return View(studentParliamentMember);
        }

        // GET: StudentParliamentMembers/Create
        public IActionResult Create()
        {
            ViewData["PositionId"] = new SelectList(_context.StudentParliamentPositions, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName");
            return View();
        }

        // POST: StudentParliamentMembers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,PositionId")] StudentParliamentMember studentParliamentMember)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentParliamentMember);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PositionId"] = new SelectList(_context.StudentParliamentPositions, "Id", "Name", studentParliamentMember.PositionId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", studentParliamentMember.StudentId);
            return View(studentParliamentMember);
        }

        // GET: StudentParliamentMember/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentParliamentMember = await _context.StudentParliamentMembers.FindAsync(id);
            if (studentParliamentMember == null)
            {
                return NotFound();
            }
            ViewData["PositionId"] = new SelectList(_context.StudentParliamentPositions, "Id", "Id", studentParliamentMember.PositionId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", studentParliamentMember.StudentId);
            return View(studentParliamentMember);
        }

        // POST: StudentParliamentMember/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,PositionId")] StudentParliamentMember studentParliamentMember)
        {
            if (id != studentParliamentMember.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentParliamentMember);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentParliamentMemberExists(studentParliamentMember.Id))
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
            ViewData["PositionId"] = new SelectList(_context.StudentParliamentPositions, "Id", "Id", studentParliamentMember.PositionId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", studentParliamentMember.StudentId);
            return View(studentParliamentMember);
        }

        // GET: StudentParliamentMember/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentParliamentMember = await _context.StudentParliamentMembers
                .Include(s => s.Position)
                .Include(s => s.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentParliamentMember == null)
            {
                return NotFound();
            }

            return View(studentParliamentMember);
        }

        // POST: StudentParliamentMember/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentParliamentMember = await _context.StudentParliamentMembers.FindAsync(id);
            if (studentParliamentMember != null)
            {
                _context.StudentParliamentMembers.Remove(studentParliamentMember);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentParliamentMemberExists(int id)
        {
            return _context.StudentParliamentMembers.Any(e => e.Id == id);
        }
    }
}
