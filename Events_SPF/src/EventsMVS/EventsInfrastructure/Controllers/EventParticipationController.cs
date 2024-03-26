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
    public class EventParticipationController : Controller
    {
        private readonly BdeventsContext _context;

        public EventParticipationController(BdeventsContext context)
        {
            _context = context;
        }

        // GET: EventParticipation
        public async Task<IActionResult> Index()
        {
            var bdeventsContext = _context.EventParticipations.Include(e => e.Event).Include(e => e.Student);
            return View(await bdeventsContext.ToListAsync());
        }

        // GET: EventParticipation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventParticipation = await _context.EventParticipations
                .Include(e => e.Event)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (eventParticipation == null)
            {
                return NotFound();
            }

            return View(eventParticipation);
        }

        // GET: EventParticipation/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName");
            return View();
        }

        // POST: EventParticipation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,EventId,Result,Id")] EventParticipation eventParticipation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventParticipation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name", eventParticipation.EventId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", eventParticipation.StudentId);
            return View(eventParticipation);
        }

        // GET: EventParticipation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventParticipation = await _context.EventParticipations.FindAsync(id);
            if (eventParticipation == null)
            {
                return NotFound();
            }
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name", eventParticipation.EventId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", eventParticipation.StudentId);
            return View(eventParticipation);
        }

        // POST: EventParticipation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,EventId,Result,Id")] EventParticipation eventParticipation)
        {
            if (id != eventParticipation.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventParticipation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventParticipationExists(eventParticipation.StudentId))
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
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Name", eventParticipation.EventId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "FullName", eventParticipation.StudentId);
            return View(eventParticipation);
        }

        // GET: EventParticipation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventParticipation = await _context.EventParticipations
                .Include(e => e.Event)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (eventParticipation == null)
            {
                return NotFound();
            }

            return View(eventParticipation);
        }

        // POST: EventParticipation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventParticipation = await _context.EventParticipations.FindAsync(id);
            if (eventParticipation != null)
            {
                _context.EventParticipations.Remove(eventParticipation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventParticipationExists(int id)
        {
            return _context.EventParticipations.Any(e => e.StudentId == id);
        }
    }
}
